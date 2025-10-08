using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Imagen
    {
        public Imagen(int idImagen, string url, int idArticulo)
        {
            this.IdImagen = idImagen;
            this.Url = url;
            this.IdArticulo = idArticulo;
        }
        public int IdImagen { get; set; }
        public string Url { get; set; }
        public int IdArticulo { get; set; }
    }
}
