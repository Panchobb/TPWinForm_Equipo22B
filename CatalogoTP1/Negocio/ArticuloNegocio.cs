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

                datos.SetearConsulta(
                    "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, ImagenUrl, IdMarca, IdCategoria) " +
                    "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @ImagenUrl, @IdMarca, @IdCategoria)");
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@ImagenUrl", nuevo.imagenes.ImagenUrl);
                datos.SetearParametro("@IdMarca", nuevo.marca.Id);
                datos.SetearParametro("@IdCategoria", nuevo.categorias.Id);

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


        public void modificado(Articulos aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta(
                    "UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, ImagenUrl = @ImagenUrl, IdMarca = @IdMarca, IdCategoria = @IdCategoria " +
                    "WHERE Id = @Id");
                datos.SetearParametro("@Codigo", aux.Codigo);
                datos.SetearParametro("@Nombre", aux.Nombre);
                datos.SetearParametro("@Descripcion", aux.Descripcion);
                datos.SetearParametro("@Precio", aux.Precio);
                datos.SetearParametro("@ImagenUrl", aux.imagenes.ImagenUrl);
                datos.SetearParametro("@IdMarca", aux.marca.Id);
                datos.SetearParametro("@IdCategoria", aux.categorias.Id);
                datos.SetearParametro("@Id", aux.Id);

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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id");
                datos.SetearParametro("@Id", id);
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