using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Imagen
    {
        public Imagen(int idArticulo, string url)
        {
            this.IdArticulo = idArticulo;
            this.Url = url;
        }
        public Imagen(int IdImagen, int idArticulo, string url)
        {
            this.IdImagen = IdImagen;
            this.IdArticulo = idArticulo;
            this.Url = url;
        }
        public int IdImagen { get; set; }
        public string Url { get; set; }
        public int IdArticulo { get; set; }
    }
}
