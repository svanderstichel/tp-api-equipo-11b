using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public Categoria Buscar(int id)
        {
            Categoria categoria = new Categoria();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearParametro("@id", id);
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS WHERE Id = @id");
                datos.ejercutarLectura();

                while (datos.Lector.Read())
                {
                    categoria.IdCategoria = (int)datos.Lector["Id"];
                    categoria.Nombre = (string)datos.Lector["Descripcion"];
                }

                return categoria;
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
