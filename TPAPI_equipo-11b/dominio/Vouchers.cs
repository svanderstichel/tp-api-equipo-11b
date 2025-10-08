using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Vouchers
    {
        public string IdVouchers { get; set; }
        
        public int IdCliente { get; set; }
        public DateTime fechaCarg {  get; set; }
        public int IdArticulo { get; set; }
        
    }
}
