using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace CatalogoTP1
{
    public partial class frmConfiguracion : Form
    {
        private List<Marcas> listaMarcas;
        private List<Categorias> listaCategorias;
        private Marcas marca = new Marcas();
        private Categorias categoria = new Categorias();
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
          
            cboOpciones.Items.Add("Categoría");
            cboOpciones.Items.Add("Marca");

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                listaMarcas = marcaNegocio.Listar();
                listaCategorias = categoriaNegocio.Listar();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas y categorías: " + ex.Message);
            }


        }

        private void actualizarLista()
        {
            string opcion = cboOpciones.Text;

            cboItem.DataSource = null;
            cboItem.DisplayMember = "Descripcion";
            cboItem.ValueMember = "Id";

            if (opcion == "Categoría")
            {
                cboItem.Items.Add("Nuevo Categoria");
                cboItem.DataSource = listaCategorias;
                
            }
            else if (opcion == "Marca")
            {
                cboItem.Items.Add("Nueva Marca");
                cboItem.DataSource = listaMarcas;
            }
          
               
        }

        private void cboOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {

            actualizarLista();
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string opcion = cboOpciones.Text;
            string descripcion = txtDescripcion.Text;

          
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese una descripción.");
                return;
            }

            try
            {
                if (opcion == "Categoría")
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    Categorias categoria = new Categorias();
                    categoria.Descripcion = descripcion;

                    negocio.agregar(categoria);

                    MessageBox.Show("Categoría agregada correctamente.");

                    listaCategorias = negocio.Listar();
                    actualizarLista();
                }
                else if (opcion == "Marca")
                {
                    MarcaNegocio negocio = new MarcaNegocio();

                    Marcas marca = new Marcas();
                    marca.Descripcion = descripcion;

                    negocio.agregar(marca);

                    MessageBox.Show("Marca agregada correctamente.");

                    listaMarcas = negocio.Listar();
                    actualizarLista();
                }

                txtDescripcion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {

            string opcion = cboOpciones.Text;
            int id = Convert.ToInt32(cboItem.SelectedValue);
            string descripcion = txtDescripcion.Text;

            try
            {

                if (opcion == "Categoría")
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    Categorias categoria = new Categorias();
                    categoria.Id = id;
                    categoria.Descripcion = descripcion;

                    negocio.modificar(categoria);
                    listaCategorias = negocio.Listar();
                    MessageBox.Show("Categoría modificada correctamente.");
                }
                else if (opcion == "Marca")
                {
                    MarcaNegocio negocio = new MarcaNegocio();

                    Marcas marca = new Marcas();
                    marca.Id = id;
                    marca.Descripcion = descripcion;

                    negocio.modificar(marca);

                    MessageBox.Show("Marca modificada correctamente.");
                }


                txtDescripcion.Clear();
            } catch(Exception ex)
            {
                MessageBox.Show("Error al eliminar " + ex.Message);
            }
         
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            string opcion = cboOpciones.Text;

           
            if (cboItem.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un ítem.");
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                int id = Convert.ToInt32(cboItem.SelectedValue);

                if (opcion == "Categoría")
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.eliminar(id);

                    MessageBox.Show("Categoría eliminada correctamente.");

                    listaCategorias = negocio.Listar();
                    actualizarLista();
                }
                else if (opcion == "Marca")
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminar(id);
                    MessageBox.Show("Marca eliminada correctamente.");

                    listaMarcas = negocio.Listar();
                    actualizarLista();
                }

                txtDescripcion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar " + ex.Message);
            }
        }
    

    }


}
