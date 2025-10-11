using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;
using System.Web.UI.WebControls.WebParts;

namespace api_productos.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
       /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            return articulo.ListarArticulos();

        }
        

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Producto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Producto/5

        /*public void Put(int id, [FromBody] ArticuloDto art)
        {
            ModificarArticuloNegocio negocio = new ModificarArticuloNegocio();
            Articulo modifica = new Articulo();
            modifica.Nombre = art.Nombre;
            modifica.Descripcion = art.Descripcion;
            modifica.imagen = art.imagen;
            modifica.Precio = art.Precio;
            modifica.Categoria = new Categoria { IdCategoria = art.idCategoria };
            modifica.Marca = new Marca { IdMarca = art.idMarca };
            modifica.CodigoArticulo = art.CodigoArticulo;
            modifica.IdArticulo = id;
            negocio.Modificar(modifica);

        }*/
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
