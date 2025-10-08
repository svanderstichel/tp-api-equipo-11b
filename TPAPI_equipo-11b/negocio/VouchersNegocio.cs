using dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace negocio
{
    public class VouchersNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public void CompletarVoucher(Vouchers nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Vouchers " +
                                     "SET IdCliente = @IdCliente, FechaCanje = @FechaCanje, IdArticulo = @IdArticulo " +
                                     "WHERE CodigoVoucher = @CodigoVoucher");
                datos.setearParametro("@IdCliente", nuevo.IdCliente);
                datos.setearParametro("@FechaCanje", nuevo.fechaCarg);
                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.setearParametro("@CodigoVoucher", nuevo.IdVouchers);

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

        public bool ValidarVoucher(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            
            
            try
            {
                datos.setearConsulta("SELECT FechaCanje FROM Vouchers WHERE CodigoVoucher = @codigo and FechaCanje IS NULL");
                datos.setearParametro("@codigo", codigo);
                datos.ejercutarLectura();

                

                if (datos.lector.Read())
                {
                                     
                      
                    return true;
                }
                else { return false; }

            }


            catch (Exception)
            {

                throw;
            }


        }
    }
}
