using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public Marca Buscar(int id)
        {
            Marca marca = new Marca();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearParametro("@id", id);
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS WHERE Id = @id");
                datos.ejercutarLectura();

                while (datos.Lector.Read())
                {
                    marca.IdMarca = (int)datos.Lector["Id"];
                    marca.Nombre = (string)datos.Lector["Descripcion"];
                }

                return marca;
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