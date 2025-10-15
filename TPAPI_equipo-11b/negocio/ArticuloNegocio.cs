using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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


            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo as Codigo, \r\nA.Nombre, \r\nA.Descripcion, \r\nM.Descripcion AS Marca, \r\nC.Descripcion AS Categoria, \r\nA.Precio, M.Id as IdMarca, C.Id as IdCategoria, I.Id as IdImagen, I.ImagenUrl AS Imagen \r\nFROM ARTICULOS A\r\nLEFT JOIN MARCAS M ON A.IdMarca = M.Id\r\nLEFT JOIN CATEGORIAS C ON A.idCategoria = C.Id \r\nLEFT JOIN IMAGENES I ON A.Id = I.IdArticulo");
                datos.ejercutarLectura();

                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    int id = (int)datos.lector["Id"];
                    Imagen imagenAux = !(datos.lector["IdImagen"] is DBNull)
                    ? new Imagen((int)datos.lector["IdImagen"], id, Convert.ToString(datos.lector["Imagen"]))
                    : null;
                    bool existe = false;

                    foreach (Articulo articulo in listaArticulos)
                    {
                        if (articulo.IdArticulo == id)
                        {
                            existe = true;
                            articulo.ListaImagenes.Add(imagenAux);
                            break;
                        }
                    }

                    if (!existe)
                    {
                        aux.IdArticulo = (int)datos.lector["Id"];
                        aux.CodigoArticulo = Convert.ToString(datos.lector["Codigo"]); // lo cambie a string pues asi està en la BD
                        aux.Nombre = Convert.ToString(datos.lector["Nombre"]);
                        aux.Descripcion = Convert.ToString(datos.lector["Descripcion"]);
                        aux.Marca = new Marca();
                        aux.Marca.Nombre = Convert.ToString(datos.lector["Marca"]);
                        aux.Categoria = new Categoria();
                        aux.Categoria.Nombre = Convert.ToString(datos.lector["Categoria"]);
                        if (!(datos.lector["Precio"] is DBNull))
                            aux.Precio = Convert.ToDecimal(datos.lector["Precio"]);
                        if (!(datos.lector["IdMarca"] is DBNull))
                            aux.Marca.IdMarca = Convert.ToInt32(datos.lector["IdMarca"]);
                        if (!(datos.lector["IdCategoria"] is DBNull))
                            aux.Categoria.IdCategoria = Convert.ToInt32(datos.lector["IdCategoria"]);
                        aux.ListaImagenes = new List<Imagen>();
                        aux.ListaImagenes.Add(imagenAux);
                    listaArticulos.Add(aux);
                    }
                }
                return listaArticulos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                datos.conexion.Close();
            }


        }
        public void AltaArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearParametro("@Codigo", articulo.CodigoArticulo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.IdMarca);
                datos.setearParametro("@IdCategoria", articulo.Categoria.IdCategoria);
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria)");
                datos.ejecutarAccion();
            }
            
            catch (Exception)
            {
                throw;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }
        public void CargarImagenes(Articulo articulo)
        {
            foreach (var imagen in articulo.ListaImagenes)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearParametro("@IdArticulo", articulo.IdArticulo);
                    datos.setearParametro("@ImagenUrl", imagen.Url);
                    datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                    datos.ejecutarAccion();
                }

                catch (Exception)
                {
                    throw;
                }

                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idmarca, IdCategoria = @idcategoria, Precio = @precio WHERE Id = @id");
                //atos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idmarca, IdCategoria = @idcategoria, Precio = @precio WHERE Id = @id");
                datos.setearParametro("@codigo", articulo.CodigoArticulo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idmarca", articulo.Marca.IdMarca);
                datos.setearParametro("@idcategoria", articulo.Categoria.IdCategoria);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.IdArticulo);
                datos.ejecutarAccion();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }



}



