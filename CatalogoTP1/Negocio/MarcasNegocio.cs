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

        public void modificar(Marcas marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "UPDATE MARCAS SET " +
                    "Descripcion = @Descripcion " +
                    "WHERE Id = @Id"
                );

                datos.SetearParametro("@Descripcion", marca.Descripcion);
                datos.SetearParametro("@Id", marca.Id);

                datos.ejecutarAccion();
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


        public void agregar(Marcas marcaNueva)
        {
            if (string.IsNullOrWhiteSpace(marcaNueva.Descripcion))
                throw new Exception("La descripción no puede estar vacía.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "INSERT INTO MARCAS (Descripcion) VALUES (@Descripcion)"
                );

                datos.SetearParametro("@Descripcion", marcaNueva.Descripcion);
                datos.ejecutarAccion();
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
                datos.SetearConsulta(
                    "DELETE FROM MARCAS WHERE Id = @Id"
                );

                datos.SetearParametro("@Id", id);

                datos.ejecutarAccion();
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