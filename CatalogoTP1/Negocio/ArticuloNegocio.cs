using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulos> listar()
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Descripcion AS Marca, c.Descripcion AS Categoria, a.Precio, a.IdMarca, a.IdCategoria, i.ImagenUrl " +
                    "FROM ARTICULOS a " +
                    "INNER JOIN MARCAS m ON m.Id = a.IdMarca " +
                    "INNER JOIN CATEGORIAS c ON c.Id = a.IdCategoria " +
                    "LEFT JOIN IMAGENES i ON i.IdArticulo = a.Id");

                datos.EjecutarLectura();
                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.marca = new Marcas { Id = (int)lector["IdMarca"], Descripcion = (string)lector["Marca"] };
                    aux.categorias = new Categorias { Id = (int)lector["IdCategoria"], Descripcion = (string)lector["Categoria"] };
                    aux.Precio = (decimal)lector["Precio"];
                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        aux.imagenes = new Imagenes();
                        aux.imagenes.ImagenUrl = (string)lector["ImagenUrl"];
                    }

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void agregar(Articulos nuevo)
        {
            if (nuevo.marca == null)
                throw new Exception("La marca no puede ser nula.");
            if (nuevo.categorias == null)
                throw new Exception("La categoría no puede ser nula.");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.SetearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) OUTPUT inserted.Id VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria)");
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@IdMarca", nuevo.marca.Id);
                datos.SetearParametro("@IdCategoria", nuevo.categorias.Id);

                
                int idArticuloGenerado = datos.ejecutarAccionScalar();

                
                if (nuevo.imagenes != null && !string.IsNullOrWhiteSpace(nuevo.imagenes.ImagenUrl))
                {
                    AccesoDatos datosImagen = new AccesoDatos();
                    try
                    {
                        datosImagen.SetearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @Url)");
                        datosImagen.SetearParametro("@IdArticulo", idArticuloGenerado);
                        datosImagen.SetearParametro("@Url", nuevo.imagenes.ImagenUrl);

                        datosImagen.ejecutarAccion();
                    }
                    finally
                    {
                        datosImagen.CerrarConexion();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from ARTICULOS where id = @id");
                datos.SetearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void modificar (Articulos modificar) {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
              "UPDATE ARTICULOS SET " +
              "Codigo = @Codigo, " +
              "Nombre = @Nombre, " +
              "Descripcion = @Descripcion, " +
              "IdMarca = @IdMarca, " +
              "IdCategoria = @IdCategoria, " +
              "Precio = @Precio " +
              "WHERE Id = @Id"
          );

                datos.SetearParametro("@Codigo", modificar.Codigo);
                datos.SetearParametro("@Nombre", modificar.Nombre);
                datos.SetearParametro("@Descripcion", modificar.Descripcion);
                datos.SetearParametro("@IdMarca", modificar.marca.Id);
                datos.SetearParametro("@IdCategoria", modificar.categorias.Id);
                datos.SetearParametro("@Precio", modificar.Precio);
                datos.SetearParametro("@Id", modificar.Id);

                datos.ejecutarAccion();



            }
            catch (Exception e)
            {

                throw e;
            }
            finally { 
             
                datos.CerrarConexion();
            }

        
        }



        public List <Articulos> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = 
    "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, " +
    "m.Descripcion AS Marca, c.Descripcion AS Categoria, " +
    "a.Precio, a.IdMarca, a.IdCategoria, i.ImagenUrl " +
    "FROM ARTICULOS a " +
    "INNER JOIN MARCAS m ON m.Id = a.IdMarca " +
    "INNER JOIN CATEGORIAS c ON c.Id = a.IdCategoria " +
    "LEFT JOIN IMAGENES i ON i.IdArticulo = a.Id " +
    "WHERE "; 


                if (campo == "numero")
                {
                    switch (criterio)
                    {

                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;

                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;

                    }

                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {


                        case "Empieza con":
                            consulta += "Nombre Like '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += "Nombre Like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre Like '%" + filtro + "%'";
                            break;


                    }
                }
                else if (campo == "Descripcion")
                {

                    switch (criterio)
                    {

                        case "Empieza con":
                            consulta += "Descripcion Like '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += "Descripcion Like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Descripcion Like '%" + filtro + "%'";
                            break;

                    }

                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {


                        case "Empieza con":
                            consulta += "Marca Like '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += "Marca Like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Marca Like '%" + filtro + "%'";
                            break;


                    }
                }
                else if (campo == "Categoria")
                {

                    switch (criterio)
                    {


                        case "Empieza con":
                            consulta += "Categoria Like '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += "Categoria Like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Categoria Like '%" + filtro + "%'";
                            break;


                    }
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.marca = new Marcas { Id = (int)datos.Lector["IdMarca"], Descripcion = (string)datos.Lector["Marca"] };
                    aux.categorias = new Categorias { Id = (int)datos.Lector["IdCategoria"], Descripcion = (string)datos.Lector["Categoria"] };
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.imagenes = new Imagenes();
                        aux.imagenes.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }

                    lista.Add(aux);
                }
                



                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}