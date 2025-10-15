using api_productos.Models;
using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

namespace api_productos.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
       public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            return articulo.ListarArticulos();

        }
        

        // GET: api/Producto/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                //buscar
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.ListarArticulos();

                Articulo articulo = lista.Find(x => x.IdArticulo == id);

                if (articulo == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe");

                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al buscar el articulo.");
            }
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
            foreach (var imagen in imagenes.ListaImagenes)
            {
                aux.ListaImagenes.Add(new Imagen(id, imagen));
            }

            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.CargarImagenes(aux);
        }
        // PUT: api/Producto/id

       public void Put(int id, [FromBody] ProductoDto art)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo modifica = new Articulo();
            modifica.CodigoArticulo = art.CodigoArticulo;
            modifica.Nombre = art.Nombre;
            modifica.Descripcion = art.Descripcion;
            modifica.Marca = new Marca { IdMarca = art.idMarca };
            modifica.Categoria = new Categoria { IdCategoria = art.idCategoria };
            modifica.Precio = art.Precio;
           
            
            
            modifica.IdArticulo = id;
            negocio.Modificar (modifica);

        }
        

        // DELETE: api/Producto/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.ListarArticulos();

                Articulo articulo = lista.Find(x => x.IdArticulo == id);

                if (articulo == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe.");

                negocio.eliminar(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Articulo eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrio un error inesperado");
            }
        }
    }
}
