using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AToko.Models
{
    public class ReportSales
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }
}