using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class ClientesNegocio
    {
        //recupero todos los registros de clientes de la base de datos
        public List<Clientes> ListarClientes()
        {
            List<Clientes> listaClientes = new List<Clientes>();
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM Clientes;");

            try
            {
                datos.ejercutarLectura();

                while (datos.lector.Read())
                {
                    Clientes clienteAux = new Clientes();

                    clienteAux.Id = (int)datos.lector["Id"];
                    clienteAux.Documento = Convert.ToString(datos.lector["Documento"]);
                    clienteAux.Nombre = Convert.ToString(datos.lector["Nombre"]);
                    clienteAux.Apellido = Convert.ToString(datos.lector["Apellido"]);
                    clienteAux.Email = Convert.ToString(datos.lector["Email"]);
                    clienteAux.Direccion = Convert.ToString(datos.lector["Direccion"]);
                    clienteAux.Ciudad = Convert.ToString(datos.lector["Ciudad"]);
                    clienteAux.CP = Convert.ToInt32(datos.lector["CP"]);

                    listaClientes.Add(clienteAux);
                }
                return listaClientes;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Clientes BuscarClientePorDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM Clientes WHERE Documento = @Documento;");
            datos.setearParametro("@Documento", dni);

            try
            {
                datos.ejercutarLectura();
                while (datos.lector.Read())
                {
                    Clientes cliente = new Clientes();

                    cliente.Id = (int)datos.lector["Id"];
                    cliente.Documento = Convert.ToString(datos.lector["Documento"]);
                    cliente.Nombre = Convert.ToString(datos.lector["Nombre"]);
                    cliente.Apellido = Convert.ToString(datos.lector["Apellido"]);
                    cliente.Email = Convert.ToString(datos.lector["Email"]);
                    cliente.Direccion = Convert.ToString(datos.lector["Direccion"]);
                    cliente.Ciudad = Convert.ToString(datos.lector["Ciudad"]);
                    cliente.CP = Convert.ToInt32(datos.lector["CP"]);

                    return cliente;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Agregar(Clientes nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                    "VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP)");
                datos.setearParametro("@Documento", nuevo.Documento);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Direccion", nuevo.Direccion);
                datos.setearParametro("@Ciudad", nuevo.Ciudad);
                datos.setearParametro("@CP", nuevo.CP);

                datos.ejecutarAccion();
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
        public void Actualizar(Clientes cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Clientes " +
                                     "SET Documento = @Documento, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Direccion = @Direccion, Ciudad = @Ciudad, CP = @CP " +
                                     "WHERE Documento = @Documento");
                datos.setearParametro("@Documento", cliente.Documento);
                datos.setearParametro("@Nombre", cliente.Nombre);
                datos.setearParametro("@Apellido", cliente.Apellido);
                datos.setearParametro("@Email", cliente.Email);
                datos.setearParametro("@Direccion", cliente.Direccion);
                datos.setearParametro("@Ciudad", cliente.Ciudad);
                datos.setearParametro("@CP", cliente.CP);

                datos.ejecutarAccion();
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

