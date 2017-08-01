using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATokoEntities
{
   public class Sale
    {
        public int SaleID { get; set; }
        public DateTime Date { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public string Notes { get; set; }



        public virtual Product Product { get; set; }
    }
}
