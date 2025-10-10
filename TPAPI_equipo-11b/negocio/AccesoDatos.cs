using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        public SqlConnection conexion;// Conector con la BD
        public SqlCommand comando; // me permite hacer Acciones luego de conectarme
        public SqlDataReader lector;// Guardo el set de datos que tomo de la BD

        public SqlDataReader Lector // lee el lector desde el exterior
        { 
            get { return lector; }
        
        }
        // método para realizar la conexion a la BD
        public AccesoDatos()
        {
            //conexion = new SqlConnection("server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true");
            conexion = new SqlConnection("server=localhost\\SQLEXPRESS01;database=CATALOGO_P3_DB;integrated security=true"); //la comento puesta uso yo
            comando = new SqlCommand();
        }
        // mètodo para realizar la consulta a la BD
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text; // Sentencia de SQL  que voy a usar
            comando.CommandText = consulta; // le mando el texto con el formato de la consulta sql
        }
        // mètodo para leer la consulta
        public void ejercutarLectura()
        {
            comando.Connection = conexion;// ejecuta el comando de la consulta

            try
            {
                conexion.Open(); // abre la conexion
                lector = comando.ExecuteReader();// realiza la lectua de datos de la BD y los guardo en "LECTOR"
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //mètodo para ejecur el insert en la tabla de la BD

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();

        }


        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);

        }

        public int BuscarDato()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                int valor = (int)comando.ExecuteScalar();

                return valor;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public string BuscarDatoString()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                string valor = (string)comando.ExecuteScalar();

                return valor;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
