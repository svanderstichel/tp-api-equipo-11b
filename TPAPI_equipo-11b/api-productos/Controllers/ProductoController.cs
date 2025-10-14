using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;
using System.Web.UI.WebControls.WebParts;
using api_productos.Models;

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
        public void Post([FromBody]ProductoDto producto)
        {
            Articulo aux = new Articulo();
            
            aux.CodigoArticulo = producto.CodigoArticulo;
            aux.Nombre = producto.Nombre;
            aux.Descripcion = producto.Descripcion;
            aux.Precio = producto.Precio;
            aux.Marca = new Marca() { IdMarca = producto.idMarca };
            aux.Categoria = new Categoria() { IdCategoria = producto.idCategoria };

            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.AltaArticulo(aux);
        }

        // POST: api/Producto/id
        public void Post(int id, [FromBody] ImagenesDto imagenes)
        {
            Articulo aux = new Articulo();
            
            aux.IdArticulo = id;
            aux.ListaImagenes = new List<Imagen>();
            foreach (var imagen in imagenes.ListaImagenes){
                aux.ListaImagenes.Add(new Imagen(id,imagen));
            }

            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.CargarImagenes(aux);
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
