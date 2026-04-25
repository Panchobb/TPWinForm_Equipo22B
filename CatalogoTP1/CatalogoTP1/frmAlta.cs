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
    public partial class frmAlta : Form
    {
        private OpenFileDialog archivo = null;
        private Articulos articulo = null;

        public frmAlta()
        {
            InitializeComponent();
            this.Load += frmAgregar_Load;
        }

        public frmAlta(Articulos articulo)
        {
            InitializeComponent();
            this.Load += frmAgregar_Load;

            this.articulo = articulo;
            Text = "Modificar Articulo";
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

                if (articulo != null)
                {
                    txtNombre.Text = articulo.Nombre;
                    txtCodigo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagenUrl.Text = articulo.imagenes.ImagenUrl;
                    CargarImagen(articulo.imagenes.ImagenUrl);
                    cbxMarca.SelectedValue = articulo.marca.Id;
                    cbxCategoria.SelectedValue = articulo.categorias.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas y categorías: " + ex.Message);
            }
        }

     

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtImagenUrl.Text);
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null) articulo = new Articulos();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.marca = (Marcas)cbxMarca.SelectedItem;
                articulo.categorias = (Categorias)cbxCategoria.SelectedItem;
                articulo.imagenes = new Imagenes { ImagenUrl = txtImagenUrl.Text };

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado exitosamente.");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Artículo agregado exitosamente.");
                }

                if (archivo != null && txtImagenUrl.Text.ToUpper().Contains("HTTP"))
                {
                    File.Copy(
                        archivo.FileName,
                        ConfigurationManager.AppSettings["carpeta-imagenes"] + archivo.FileName
                    );
                }

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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAgregarImagen_Click_1(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = archivo.FileName;
                CargarImagen(archivo.FileName);
            }

        }
    }
}

