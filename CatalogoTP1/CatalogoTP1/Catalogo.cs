using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dominio;
using Negocio;
using System.Security.Cryptography;

namespace CatalogoTP1
{
    public partial class Catalogo : Form
    {
        private List<Articulos> listaArticulos;
        private int indiceImagen = 0;

        public Catalogo()
        {
            InitializeComponent();
            DgvArticulos.SelectionChanged += DgvArticulos_SelectionChanged;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulos = negocio.listar();
                DgvArticulos.DataSource = listaArticulos;

                foreach (DataGridViewColumn col in DgvArticulos.Columns)
                {
                    if (col.Name.ToUpper() == "ID" || col.Name.ToUpper() == "IMAGENES")
                    {
                        col.Visible = false;
                    }
                }
                PbxArticulos.SizeMode = PictureBoxSizeMode.Zoom;

                cboCampo.Items.Add("Codigo");
          
                cboCampo.Items.Add("Nombre");
                cboCampo.Items.Add("Marca");
                cboCampo.Items.Add("Categoria");
                cboCampo.Items.Add("Descripcion");
                cboCampo.Items.Add("Precio");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el catálogo: " + ex.ToString());
            }
        }

        private void Cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                listaArticulos = negocio.listar();


                DgvArticulos.DataSource = null;


                DgvArticulos.DataSource = listaArticulos;


                DgvArticulos.Columns["Id"].Visible = false;
                DgvArticulos.Columns["Imagenes"].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void DgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            indiceImagen = 0;

            if (DgvArticulos.CurrentRow == null || DgvArticulos.CurrentRow.DataBoundItem == null)
                return;

            Articulos seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
            txbCodigo.Text = seleccionado.Codigo;
            txbNombre.Text = seleccionado.Nombre;
            txbDescripcion.Text = seleccionado.Descripcion;
            txbCategoria.Text = seleccionado.categorias.Descripcion;
            txbMarca.Text = seleccionado.marca.Descripcion;
            txbPrecio.Text= seleccionado.Precio.ToString("0.00");

            if (seleccionado.imagenes != null && seleccionado.imagenes.Count > 0 && !string.IsNullOrWhiteSpace(seleccionado.imagenes[0].ImagenUrl))
            {
                CargarImagen(seleccionado.imagenes[0].ImagenUrl);
            }
            else
            {
                CargarImagen("");
            }
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                PbxArticulos.Load(imagen);
            }
            catch (Exception)
            {

                PbxArticulos.Load("https://capacitacion.fundacionbancopampa.com.ar/wp-content/uploads/2024/09/placeholder-4.png");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAlta alta = new frmAlta();
            alta.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulos seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    Cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();


            string campo = cboCampo.Text;
            string criterio = cboCriterio.Text;
            string filtro = textBox1.Text.ToUpper();

            if (campo == "" && criterio == "")
            {
                List<Articulos> listaFiltrada;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    listaFiltrada = listaArticulos;
                }
                else
                {
                    listaFiltrada = listaArticulos.FindAll(x =>
                        x.Codigo.ToUpper().Contains(filtro) ||
                        x.Nombre.ToUpper().Contains(filtro) ||
                        x.Descripcion.ToUpper().Contains(filtro) ||
                        x.marca.Descripcion.ToUpper().Contains(filtro) ||
                        x.categorias.Descripcion.ToUpper().Contains(filtro)
                    );
                }
                DgvArticulos.DataSource = null;
                DgvArticulos.DataSource = listaFiltrada;
                if (DgvArticulos.Columns["Id"] != null)
                    DgvArticulos.Columns["Id"].Visible = false;

                if (DgvArticulos.Columns["Imagenes"] != null)
                    DgvArticulos.Columns["Imagenes"].Visible = false;

            } else
            {

                try
                {

                    DgvArticulos.DataSource = articuloNegocio.Filtrar(campo, criterio, filtro);


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            Articulos seleccionado;
            seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
            frmAlta modificar = new frmAlta(seleccionado);
            modificar.ShowDialog();
            Cargar();
        }



        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();

            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            } else if (opcion == "Nombre")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            } else if (opcion == "Marca")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            } else if (opcion == "Categoria")

            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            } else if (opcion == "Descripcion")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            } else if (opcion == "Codigo")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");

            }
          
        }

        private void btnDetalle_Click(object sender, EventArgs e)

        {
            if (DgvArticulos.CurrentRow == null || DgvArticulos.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione un artículo para ver el detalle.");
                return;
            }
            Articulos seleccionado;
            seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle mostrar = new frmDetalle(seleccionado);
            mostrar.ShowDialog();

            Cargar();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            frmConfiguracion configuracion = new frmConfiguracion();
            configuracion.ShowDialog();
            Cargar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Articulos seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;

            if (indiceImagen > 0)
            {
                indiceImagen--;
                CargarImagen(seleccionado.imagenes[indiceImagen].ImagenUrl);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Articulos seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;

            if (indiceImagen < seleccionado.imagenes.Count - 1)
            {
                indiceImagen++;
                CargarImagen(seleccionado.imagenes[indiceImagen].ImagenUrl);
            }
        }
    }
}