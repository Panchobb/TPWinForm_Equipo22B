using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

    }
}