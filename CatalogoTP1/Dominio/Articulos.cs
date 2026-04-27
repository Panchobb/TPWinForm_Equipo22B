using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulos
    {
        public int Id { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Marca")]
        public Marcas marca { get; set; }

        [DisplayName("Categoría")]
        public Categorias categorias { get; set; }

        public decimal Precio { get; set; }
        public List<Imagenes> imagenes { get; set; }


    }
}
