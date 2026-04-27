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
                    "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Descripcion AS Marca, " +
                     "c.Descripcion AS Categoria, a.Precio, a.IdMarca, a.IdCategoria " +
                     "FROM ARTICULOS a " +
                     "INNER JOIN MARCAS m ON m.Id = a.IdMarca " +
                     "INNER JOIN CATEGORIAS c ON c.Id = a.IdCategoria");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.marca = new Marcas
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Descripcion = (string)datos.Lector["Marca"]
                    };
                    aux.categorias = new Categorias
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Descripcion = (string)datos.Lector["Categoria"]
                    };

                    aux.imagenes = listarImagenes(aux.Id);

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

        public List<Imagenes> listarImagenes(int idArticulo)
        {
            List<Imagenes> lista = new List<Imagenes>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.SetearParametro("@idArticulo", idArticulo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagenes aux = new Imagenes();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

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


        public void Agregar(Articulos nuevo)
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


                if (nuevo.imagenes != null && nuevo.imagenes.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(nuevo.imagenes[0].ImagenUrl))
                    {
                        AccesoDatos datosImagen = new AccesoDatos();
                    try
                    {
                        datosImagen.SetearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @Url)");
                        datosImagen.SetearParametro("@IdArticulo", idArticuloGenerado);
                        datosImagen.SetearParametro("@Url", nuevo.imagenes[0].ImagenUrl);

                        datosImagen.ejecutarAccion();
                    }
                    finally
                    {
                        datosImagen.CerrarConexion();
                    }
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

        public void Eliminar(int id)
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


        public void Modificar (Articulos modificar) {

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
              "WHERE Id = @Id");

                datos.SetearParametro("@Codigo", modificar.Codigo);
                datos.SetearParametro("@Nombre", modificar.Nombre);
                datos.SetearParametro("@Descripcion", modificar.Descripcion);
                datos.SetearParametro("@IdMarca", modificar.marca.Id);
                datos.SetearParametro("@IdCategoria", modificar.categorias.Id);
                datos.SetearParametro("@Precio", modificar.Precio);
                datos.SetearParametro("@Id", modificar.Id);

                datos.ejecutarAccion();

                if (modificar.imagenes != null && modificar.imagenes.Count > 0)
                {
                    AccesoDatos datosImagen = new AccesoDatos();
                    try
                    {
                        datosImagen.SetearConsulta("UPDATE IMAGENES SET ImagenUrl = @Url WHERE IdArticulo = @IdArticulo");
                        datosImagen.SetearParametro("@Url", modificar.imagenes[0].ImagenUrl);
                        datosImagen.SetearParametro("@IdArticulo", modificar.Id);

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

        public List<Articulos> Filtrar(string campo, string criterio, string filtro)
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

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.Precio > " + filtro;
                            break;

                        case "Menor a":
                            consulta += "a.Precio < " + filtro;
                            break;

                        default:
                            consulta += "a.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Nombre LIKE '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "a.Nombre LIKE '%" + filtro + "'";
                            break;

                        default:
                            consulta += "a.Nombre LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Descripcion")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Descripcion LIKE '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "a.Descripcion LIKE '%" + filtro + "'";
                            break;

                        default:
                            consulta += "a.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "m.Descripcion LIKE '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "m.Descripcion LIKE '%" + filtro + "'";
                            break;

                        default:
                            consulta += "m.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "c.Descripcion LIKE '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "c.Descripcion LIKE '%" + filtro + "'";
                            break;

                        default:
                            consulta += "c.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                } else if (campo == "Codigo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Codigo LIKE '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "a.Codigo LIKE '%" + filtro + "'";
                            break;

                        default:
                            consulta += "a.Codigo LIKE '%" + filtro + "%'";
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

                    aux.marca = new Marcas
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Descripcion = (string)datos.Lector["Marca"]
                    };

                    aux.categorias = new Categorias
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Descripcion = (string)datos.Lector["Categoria"]
                    };

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.imagenes = new List<Imagenes>();

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        Imagenes img = new Imagenes();
                        img.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                        aux.imagenes.Add(img);
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
    }
}