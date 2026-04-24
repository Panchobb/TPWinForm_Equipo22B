using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatalogoTP1
{
    public partial class frmAgregar : Form
    {
        private Articulos Articulos = null;
        private OpenFileDialog archivo = null;
        public frmAgregar()
        {
            InitializeComponent();
            this.Load += frmAgregar_Load;



        }
        public frmAgregar(Articulos articulo)
        {
            InitializeComponent();
            this.Load += frmAgregar_Load;
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();
            cbxMarca.SelectedValue = articulo.marca.Id;
            cbxCategoria.SelectedValue = articulo.categorias.Id;
            txtImagenUrl.Text = articulo.imagenes.ImagenUrl;
        }
        private void frmAgregar_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cbxMarca.DataSource = marcaNegocio.Listar();
                cbxMarca.DisplayMember = "Descripcion";
                cbxMarca.ValueMember = "Id";
                cbxCategoria.DataSource = categoriaNegocio.Listar();
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas y categorías: " + ex.Message);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            Articulos nuevo = new Articulos();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.marca = (Marcas)cbxMarca.SelectedItem;
                nuevo.categorias = (Categorias)cbxCategoria.SelectedItem;
                nuevo.imagenes = new Imagenes { ImagenUrl = txtImagenUrl.Text };
                negocio.agregar(nuevo);
                MessageBox.Show("Artículo agregado exitosamente.");
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("El precio debe ser un número válido.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el artículo: " + ex.Message);
            }
            if (archivo != null && txtImagenUrl.Text.ToUpper().Contains("HTTP"))
            {
                File.Copy(archivo.FileName, ConfigurationManager.AppSettings["carpeta-imagenes"] + archivo.FileName);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAgregarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo = new OpenFileDialog();
            archivo.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = archivo.FileName;
                CargarImagen(archivo.FileName);

                

            }

        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtImagenUrl.Text);
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                pxbArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pxbArticulo.Load("https://capacitacion.fundacionbancopampa.com.ar/wp-content/uploads/2024/09/placeholder-4.png");
            }


        }

    }
}
