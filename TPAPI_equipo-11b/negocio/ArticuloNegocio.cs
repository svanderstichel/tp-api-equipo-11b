using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListarArticulos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("SELECT A.Id as idArticulo, I.Id as idImagen, Nombre, Descripcion, ImagenUrl FROM ARTICULOS A LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo;");

            try
            {
                datos.ejercutarLectura();

                while (datos.lector.Read())
                {
                    Articulo articuloAux = new Articulo();
                    int id = (int)datos.lector["idArticulo"];
                    Imagen imagenAux = new Imagen((int)datos.lector["idImagen"], Convert.ToString(datos.lector["ImagenUrl"]), id);
                    bool existe = false;

                    foreach (Articulo articulo in listaArticulos) {
                        if (articulo.IdArticulo == id)
                        {
                            existe = true;
                            articulo.ListaImagenes.Add(imagenAux);
                            break;
                        }
                    }

                    if (!existe)
                    {
                        articuloAux.IdArticulo = id;
                        articuloAux.Nombre = Convert.ToString(datos.lector["Nombre"]);
                        articuloAux.Descripcion = Convert.ToString(datos.lector["Descripcion"]);
                        articuloAux.ListaImagenes = new List<Imagen>();
                        articuloAux.ListaImagenes.Add(imagenAux);
                        listaArticulos.Add(articuloAux);
                    }
                }
                return listaArticulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

