using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace CatalogoTP1
{
    public partial class frmDetalle : Form
    {
        Articulos articulo = null;
        public frmDetalle()
        {
            InitializeComponent();
        }

        public frmDetalle(Articulos articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            
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


        private void frmDetalle_Load(object sender, EventArgs e)
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
                    if(articulo.imagenes != null)  CargarImagen(articulo.imagenes.ImagenUrl);
                    cbxMarca.SelectedValue = articulo.marca.Id;
                    cbxCategoria.SelectedValue = articulo.categorias.Id;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas y categorías: " + ex.Message);
            }


        }
    }
}
