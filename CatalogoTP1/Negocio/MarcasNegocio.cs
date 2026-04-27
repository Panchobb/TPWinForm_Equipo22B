using Dominio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marcas> Listar()
        {
            var lista = new List<Marcas>();
            var datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Marcas aux = new Marcas();
                    aux.Id = datos.Lector["Id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Id"]) : 0;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? Convert.ToString(datos.Lector["Descripcion"]) : string.Empty;
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