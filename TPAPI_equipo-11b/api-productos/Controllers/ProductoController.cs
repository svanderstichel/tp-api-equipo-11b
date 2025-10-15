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
       public HttpResponseMessage Get()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.ListarArticulos();

                if (lista == null || lista.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulos cargados.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, lista);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al obtener la lista de productos.");
            }


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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "El articulo no existe");

                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al buscar el articulo.");
            }
        }

        // POST: api/Producto
        public HttpResponseMessage Post([FromBody]ProductoDto producto)
        {
            Articulo aux = new Articulo();

            //validar que exista la marca y categoria
            CategoriaNegocio validarCategoria = new CategoriaNegocio();
            if((validarCategoria.Buscar(producto.idCategoria)).IdCategoria==0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoria no existe.");

            MarcaNegocio validarMarca = new MarcaNegocio();
            if ((validarMarca.Buscar(producto.idMarca)).IdMarca == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La Marca no existe.");

            try
            {
                aux.CodigoArticulo = producto.CodigoArticulo;
                aux.Nombre = producto.Nombre;
                aux.Descripcion = producto.Descripcion;
                aux.Precio = producto.Precio;
                aux.Marca = new Marca() { IdMarca = producto.idMarca };
                aux.Categoria = new Categoria() { IdCategoria = producto.idCategoria };

                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.AltaArticulo(aux);
                return Request.CreateResponse(HttpStatusCode.OK, "Producto agregado correctamente.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        // POST: api/Producto/id
        public HttpResponseMessage Post(int id, [FromBody] ImagenesDto imagenes)
        {
            Articulo aux = new Articulo();

            //validar que exista el articulo
            ArticuloNegocio validarArticulo= new ArticuloNegocio();
            if ((validarArticulo.Buscar(id)).IdArticulo == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe.");

            try
            {
                aux.IdArticulo = id;
                aux.ListaImagenes = new List<Imagen>();
                foreach (var imagen in imagenes.ListaImagenes)
                {
                    aux.ListaImagenes.Add(new Imagen(id, imagen));
                }
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.CargarImagenes(aux);
                return Request.CreateResponse(HttpStatusCode.OK, "Imagenes agregadas correctamente.");
            }

            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }
        // PUT: api/Producto/id

       public HttpResponseMessage Put(int id, [FromBody] ProductoDto art)
        {
            //validar que exista el articulo
            ArticuloNegocio validarArticulo = new ArticuloNegocio();
            if ((validarArticulo.Buscar(id)).IdArticulo == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe.");

            //validar que exista la marca y categoria
            CategoriaNegocio validarCategoria = new CategoriaNegocio();
            if ((validarCategoria.Buscar(art.idCategoria)).IdCategoria == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoria no existe.");

            MarcaNegocio validarMarca = new MarcaNegocio();
            if ((validarMarca.Buscar(art.idMarca)).IdMarca == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La Marca no existe.");

            try
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
                negocio.Modificar(modifica);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado correctamente.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }

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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "El articulo no existe.");

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
