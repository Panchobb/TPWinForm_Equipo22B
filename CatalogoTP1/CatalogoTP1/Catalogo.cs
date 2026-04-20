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

  
        private void DgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (DgvArticulos.CurrentRow == null || DgvArticulos.CurrentRow.DataBoundItem == null)
                return;

            Articulos seleccionado = (Articulos)DgvArticulos.CurrentRow.DataBoundItem;

  
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
               
                PbxArticulos.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}