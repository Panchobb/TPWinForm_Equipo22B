using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Marcas marca { get; set; }
        public Categorias categorias { get; set; }

        public decimal Precio { get; set; }
        
        public Imagenes Imagenes { get; set; }

        
    }
}
