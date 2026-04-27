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

        public Catalogo()
        {
            InitializeComponent();
            DgvArticulos.SelectionChanged += DgvArticulos_SelectionChanged;
        

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            listaArticulos = negocio.listar();
            DgvArticulos.DataSource = listaArticulos;

            DgvArticulos.Columns["Id"].Visible = false;
            DgvArticulos.Columns["Imagenes"].Visible = false;
            PbxArticulos.SizeMode = PictureBoxSizeMode.Zoom;
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
            if (DgvArticulos.CurrentRow == null || DgvArticulos.CurrentRow.DataBoundItem == null)
                return;

            Articulos seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
            txbCodigo.Text = seleccionado.Codigo;
            txbNombre.Text = seleccionado.Nombre;
            txbDescripcion.Text = seleccionado.Descripcion;
            txbCategoria.Text = seleccionado.categorias.Descripcion;
            txbMarca.Text = seleccionado.marca.Descripcion;
            txbPrecio.Text= seleccionado.Precio.ToString("0.00");

  
            if (seleccionado.imagenes != null && !string.IsNullOrWhiteSpace(seleccionado.imagenes.ImagenUrl))
            {
                CargarImagen(seleccionado.imagenes.ImagenUrl);
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
            frmAgregar alta = new frmAgregar();
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
                    negocio.eliminar(seleccionado.Id);
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
            List<Articulos> listaFiltrada;
            string filtro = textBox1.Text.ToUpper();

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
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            Articulos seleccionado;
            seleccionado= (Articulos)DgvArticulos.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(seleccionado);
            modificar.ShowDialog();
            Cargar();
        }
    }
}