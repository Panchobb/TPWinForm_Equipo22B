using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categorias> Listar()
        {
            var lista = new List<Categorias>();
            var datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Categorias aux = new Categorias();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
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


        public void Modificar(Categorias categoria)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                       "UPDATE CATEGORIAS SET " +
                           "Descripcion = @Descripcion " +
                                "WHERE Id = @Id");

                datos.SetearParametro("@Descripcion", categoria.Descripcion);
                datos.SetearParametro("@Id", categoria.Id);
                datos.ejecutarAccion();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {

                datos.CerrarConexion();
            }


        }


        public void Agregar(Categorias categoriaNueva)
        {

            if (categoriaNueva.Descripcion == null)
                throw new Exception("La descripcion no puede estar vacía.");

            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.SetearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@Descripcion)");
                datos.SetearParametro("@Descripcion", categoriaNueva.Descripcion);
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from CATEGORIAS where id = @id");
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