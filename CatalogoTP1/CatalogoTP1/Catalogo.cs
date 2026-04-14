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
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dataGridView1.DataSource = negocio.listar();

        }
        

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dataGridView1.DataSource = negocio.listar();
        }
    }
}
