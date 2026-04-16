using Dominio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagenes> Listar()
        {
            var lista = new List<Imagenes>();
            var datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagenes aux = new Imagenes();
                    aux.Id = datos.Lector["Id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Id"]) : 0;
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? Convert.ToString(datos.Lector["ImagenUrl"]) : string.Empty;
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