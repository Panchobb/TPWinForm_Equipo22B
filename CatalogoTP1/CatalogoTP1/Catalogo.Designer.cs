namespace CatalogoTP1
{
    partial class Catalogo
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.DgvArticulos = new System.Windows.Forms.DataGridView();
            this.PbxArticulos = new System.Windows.Forms.PictureBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.BtnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFiltro = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.Label();
            this.txtCategoria = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.Label();
            this.txbCodigo = new System.Windows.Forms.TextBox();
            this.txbNombre = new System.Windows.Forms.TextBox();
            this.txbDescripcion = new System.Windows.Forms.TextBox();
            this.txbCategoria = new System.Windows.Forms.TextBox();
            this.txbMarca = new System.Windows.Forms.TextBox();
            this.txbPrecio = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.Label();
            this.LBLDETALLE = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DgvArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxArticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvArticulos
            // 
            this.DgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvArticulos.Location = new System.Drawing.Point(12, 41);
            this.DgvArticulos.Name = "DgvArticulos";
            this.DgvArticulos.ReadOnly = true;
            this.DgvArticulos.Size = new System.Drawing.Size(664, 379);
            this.DgvArticulos.TabIndex = 0;
            // 
            // PbxArticulos
            // 
            this.PbxArticulos.Location = new System.Drawing.Point(737, 53);
            this.PbxArticulos.Name = "PbxArticulos";
            this.PbxArticulos.Size = new System.Drawing.Size(208, 161);
            this.PbxArticulos.TabIndex = 1;
            this.PbxArticulos.TabStop = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 435);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnModificar
            // 
            this.BtnModificar.Location = new System.Drawing.Point(105, 435);
            this.BtnModificar.Name = "BtnModificar";
            this.BtnModificar.Size = new System.Drawing.Size(75, 23);
            this.BtnModificar.TabIndex = 8;
            this.BtnModificar.Text = "Modificar";
            this.BtnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(204, 435);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filtro rapido";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 20);
            this.textBox1.TabIndex = 6;
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(324, 12);
            this.btnFiltro.Name = "btnFiltro";
            this.btnFiltro.Size = new System.Drawing.Size(75, 23);
            this.btnFiltro.TabIndex = 7;
            this.btnFiltro.Text = "Buscar";
            this.btnFiltro.UseVisualStyleBackColor = true;
            this.btnFiltro.Click += new System.EventHandler(this.btnFiltro_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.AutoSize = true;
            this.txtCodigo.Location = new System.Drawing.Point(706, 243);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(40, 13);
            this.txtCodigo.TabIndex = 9;
            this.txtCodigo.Text = "Codigo";
            // 
            // txtNombre
            // 
            this.txtNombre.AutoSize = true;
            this.txtNombre.Location = new System.Drawing.Point(702, 269);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(44, 13);
            this.txtNombre.TabIndex = 10;
            this.txtNombre.Text = "Nombre";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.AutoSize = true;
            this.txtDescripcion.Location = new System.Drawing.Point(683, 296);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(63, 13);
            this.txtDescripcion.TabIndex = 11;
            this.txtDescripcion.Text = "Descripcion";
            // 
            // txtCategoria
            // 
            this.txtCategoria.AutoSize = true;
            this.txtCategoria.Location = new System.Drawing.Point(694, 322);
            this.txtCategoria.Name = "txtCategoria";
            this.txtCategoria.Size = new System.Drawing.Size(52, 13);
            this.txtCategoria.TabIndex = 12;
            this.txtCategoria.Text = "Categoria";
            // 
            // txtMarca
            // 
            this.txtMarca.AutoSize = true;
            this.txtMarca.Location = new System.Drawing.Point(709, 348);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(37, 13);
            this.txtMarca.TabIndex = 13;
            this.txtMarca.Text = "Marca";
            // 
            // txbCodigo
            // 
            this.txbCodigo.Enabled = false;
            this.txbCodigo.Location = new System.Drawing.Point(763, 236);
            this.txbCodigo.Name = "txbCodigo";
            this.txbCodigo.ReadOnly = true;
            this.txbCodigo.Size = new System.Drawing.Size(167, 20);
            this.txbCodigo.TabIndex = 14;
            // 
            // txbNombre
            // 
            this.txbNombre.Location = new System.Drawing.Point(763, 262);
            this.txbNombre.Name = "txbNombre";
            this.txbNombre.ReadOnly = true;
            this.txbNombre.Size = new System.Drawing.Size(167, 20);
            this.txbNombre.TabIndex = 15;
            // 
            // txbDescripcion
            // 
            this.txbDescripcion.Location = new System.Drawing.Point(764, 289);
            this.txbDescripcion.Name = "txbDescripcion";
            this.txbDescripcion.ReadOnly = true;
            this.txbDescripcion.Size = new System.Drawing.Size(167, 20);
            this.txbDescripcion.TabIndex = 16;
            // 
            // txbCategoria
            // 
            this.txbCategoria.Location = new System.Drawing.Point(764, 315);
            this.txbCategoria.Name = "txbCategoria";
            this.txbCategoria.ReadOnly = true;
            this.txbCategoria.Size = new System.Drawing.Size(167, 20);
            this.txbCategoria.TabIndex = 17;
            // 
            // txbMarca
            // 
            this.txbMarca.Location = new System.Drawing.Point(764, 341);
            this.txbMarca.Name = "txbMarca";
            this.txbMarca.ReadOnly = true;
            this.txbMarca.Size = new System.Drawing.Size(167, 20);
            this.txbMarca.TabIndex = 18;
            // 
            // txbPrecio
            // 
            this.txbPrecio.Location = new System.Drawing.Point(763, 367);
            this.txbPrecio.Name = "txbPrecio";
            this.txbPrecio.ReadOnly = true;
            this.txbPrecio.Size = new System.Drawing.Size(167, 20);
            this.txbPrecio.TabIndex = 20;
            // 
            // txtPrecio
            // 
            this.txtPrecio.AutoSize = true;
            this.txtPrecio.Location = new System.Drawing.Point(708, 374);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(37, 13);
            this.txtPrecio.TabIndex = 19;
            this.txtPrecio.Text = "Precio";
            // 
            // LBLDETALLE
            // 
            this.LBLDETALLE.AutoSize = true;
            this.LBLDETALLE.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLDETALLE.LinkColor = System.Drawing.Color.Black;
            this.LBLDETALLE.Location = new System.Drawing.Point(720, 9);
            this.LBLDETALLE.Name = "LBLDETALLE";
            this.LBLDETALLE.Size = new System.Drawing.Size(113, 33);
            this.LBLDETALLE.TabIndex = 21;
            this.LBLDETALLE.TabStop = true;
            this.LBLDETALLE.Text = "Detalle";
            // 
            // Catalogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 463);
            this.Controls.Add(this.LBLDETALLE);
            this.Controls.Add(this.txbPrecio);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txbMarca);
            this.Controls.Add(this.txbCategoria);
            this.Controls.Add(this.txbDescripcion);
            this.Controls.Add(this.txbNombre);
            this.Controls.Add(this.txbCodigo);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.txtCategoria);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnFiltro);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.BtnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.PbxArticulos);
            this.Controls.Add(this.DgvArticulos);
            this.Name = "Catalogo";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxArticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvArticulos;
        private System.Windows.Forms.PictureBox PbxArticulos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button BtnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFiltro;
        private System.Windows.Forms.Label txtCodigo;
        private System.Windows.Forms.Label txtNombre;
        private System.Windows.Forms.Label txtDescripcion;
        private System.Windows.Forms.Label txtCategoria;
        private System.Windows.Forms.Label txtMarca;
        private System.Windows.Forms.TextBox txbCodigo;
        private System.Windows.Forms.TextBox txbNombre;
        private System.Windows.Forms.TextBox txbDescripcion;
        private System.Windows.Forms.TextBox txbCategoria;
        private System.Windows.Forms.TextBox txbMarca;
        private System.Windows.Forms.TextBox txbPrecio;
        private System.Windows.Forms.Label txtPrecio;
        private System.Windows.Forms.LinkLabel LBLDETALLE;
    }
}

