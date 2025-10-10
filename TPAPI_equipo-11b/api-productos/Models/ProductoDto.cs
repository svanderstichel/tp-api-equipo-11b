using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_productos.Models
{
    public class ProductoDto
    {
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }


        public string Descripcion { get; set; }

        public int idMarca { get; set; }
        public int idCategoria { get; set; }
        public string imagen { get; set; }
        public decimal Precio { get; set; }
    }
}