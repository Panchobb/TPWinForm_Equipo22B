using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class NegocioArticulo
    {
        public List<Articulos> listar() // Cambiado a 'Articulo' en singular, asumiendo que corregiste el nombre de la clase
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // La consulta está perfecta
                datos.SetearConsulta(
                     "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Descripcion AS Marca, c.Descripcion AS Categoria, a.Precio, a.IdMarca, a.IdCategoria, i.ImagenUrl " +
                     "FROM ARTICULOS a " +
                     "INNER JOIN MARCAS m ON a.IdMarca = m.Id " +
                     "INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id " +
                     "LEFT JOIN IMAGENES i ON a.Id = i.IdArticulo");

                datos.EjecutarLectura();
                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)lector["Id"]; 
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.marca = new Marcas();
                    aux.marca.Id = (int)lector["IdMarca"];
                    aux.marca.Descripcion = (string)lector["Marca"];
                    aux.categorias= new Categorias();
                    aux.categorias.Id = (int)lector["IdCategoria"];
                    aux.categorias.Descripcion = (string)lector["Categoria"];
                    aux.Precio = (decimal)lector["Precio"];
                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagenes = new Imagenes();
                        aux.Imagenes.ImagenUrl = (string)lector["ImagenUrl"];
                    }

                    lista.Add(aux);
                }

                return lista;
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