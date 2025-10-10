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
                datos.setearConsulta ("SELECT A.Id, A.Codigo as Codigo, \r\nA.Nombre, \r\nA.Descripcion, \r\nM.Descripcion AS Marca, \r\nC.Descripcion AS Categoria, \r\nA.Precio, M.Id as IdMarca, C.Id as IdCategoria \r\nFROM ARTICULOS A\r\nLEFT JOIN MARCAS M ON A.IdMarca = M.Id\r\nLEFT JOIN CATEGORIAS C ON A.idCategoria = C.Id");
                datos.ejercutarLectura();
                                
                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();

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

                    listaArticulos.Add(aux);
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
    }
}


