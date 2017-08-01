using AToko.DataContexts;
using AToko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AToko.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private ATokoDb db = new ATokoDb();
        // GET: Report

        [Authorize(Roles = "admin")]
        public ActionResult ReportIn()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportIns(string date1 , string date2)
        {
            if (date1 != null && date2 != null)
            {
                //string query = "select  ProductIns.ProductInID , ProductIns.Date,ProductIns.ProductCode ,ProductIns.Notes,Products.ProductName, ProductIns.Qty  ,ProductIns.Price, ProductIns.Price*ProductIns.Qty as Total from ProductIns JOIN Products on ProductIns.ProductCode = Products.ProductCode where ProductIns.Date >= '" + date1 + "' AND  ProductIns.Date <='" + date2 + "'";
                string query = string.Format("EXEC [dbo].[sp_GetReportIn] @dateFrom = '{0}', @dateTo = '{1}'", date1, date2);
                IEnumerable<Report> reportin = db.Database.SqlQuery<Report>(query);
                ViewBag.reportin = reportin.ToList();
            }

            return PartialView();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportOut()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportOuts(string date1 , string date2)
        {
            if (date1 != null && date2 != null)
            {
                //string query = "select ProductOuts.ProductOutID,ProductOuts.Date, ProductOuts.ProductCode ,ProductOuts.Notes,Products.ProductName, Products.Price , ProductOuts.Qty ,Products.Price*ProductOuts.Qty as Total FROM ProductOuts JOIN Products on ProductOuts.ProductCode=Products.ProductCode where ProductOuts.Date >='" + date1 + "' AND ProductOuts.Date <= '" + date2 + "'";
                string query = string.Format("EXEC [dbo].[sp_GetReportOut] @dateFrom = '{0}', @dateTo = '{1}'", date1, date2);
                IEnumerable<Report> reportout = db.Database.SqlQuery<Report>(query);
                ViewBag.reportout = reportout.ToList();
            }

            return PartialView();
        }

        
        public ActionResult ReportStock()
        {
            var supplierName = User.Identity.Name;
            //var datetoday = System.DateTime.Today;
            //string query = "select ProductIns.ProductCode  , SUM (ProductIns.Qty) as ProductIn ,  SUM (ProductOuts.Qty) as ProductOut  ,SUM (ProductIns.Qty) -  SUM (ProductOuts.Qty) as Stock from ProductIns join ProductOuts on ProductIns.ProductCode=ProductIns.ProductCode where ProductIns.Date <= '"+datetoday+"' AND ProductOuts.Date <= '"+datetoday+"' group by ProductIns.ProductCode";

            string query = string.Empty;

            if (User.IsInRole("admin"))
            {
                query = "EXEC [dbo].[sp_GetReportStock] @dateFrom = NULL, @dateTo = NULL";
            }
            else
            {
                query = string.Format("EXEC [dbo].[sp_GetReportStock] @dateFrom = NULL, @dateTo = NULL, @supplierName = '{0}'", supplierName);
            }
            
            IEnumerable<ReportStock> stocktoday = db.Database.SqlQuery<ReportStock>(query);
            ViewBag.reporttoday = stocktoday.ToList();

            return View();
        }

        public ActionResult ReportStocks(string date1, string date2)
        {
            var supplierName = User.Identity.Name;

            if (date1 != null && date2 != null)
            {
                //string query = "select ProductIns.ProductCode  , SUM (ProductIns.Qty) as ProductIn ,  SUM (ProductOuts.Qty) as ProductOut  ,SUM (ProductIns.Qty) -  SUM (ProductOuts.Qty) as Stock from ProductIns join ProductOuts on ProductIns.ProductCode=ProductIns.ProductCode where ProductIns.Date >= '"+date1+"' AND ProductIns.Date <= '"+date2+"' AND ProductOuts.Date >= '"+date1+"' AND ProductOuts.Date <= '"+date2+"' group by ProductIns.ProductCode ";
                string query = string.Empty;

                if (User.IsInRole("admin"))
                {
                    query = string.Format("EXEC [dbo].[sp_GetReportStock] @dateFrom = '{0}', @dateTo = '{1}'", date1, date2);
                }
                else
                {
                    query = string.Format("EXEC [dbo].[sp_GetReportStock] @dateFrom = '{0}', @dateTo = '{1}', @supplierName = '{2}'", date1, date2, supplierName);
                }

                IEnumerable<ReportStock> reportstock = db.Database.SqlQuery<ReportStock>(query);
                ViewBag.reportstock = reportstock.ToList();
            }

            return PartialView();
        }


        public ActionResult ReportSale()
        {
            var supplierName = User.Identity.Name;
            //var datetoday = System.DateTime.Today;
            //string query = "select ProductIns.ProductCode  , SUM (ProductIns.Qty) as ProductIn ,  SUM (ProductOuts.Qty) as ProductOut  ,SUM (ProductIns.Qty) -  SUM (ProductOuts.Qty) as Stock from ProductIns join ProductOuts on ProductIns.ProductCode=ProductIns.ProductCode where ProductIns.Date <= '"+datetoday+"' AND ProductOuts.Date <= '"+datetoday+"' group by ProductIns.ProductCode";

            string query = string.Empty;

            if (User.IsInRole("admin"))
            {
                query = "EXEC [dbo].[sp_GetReportSales] @dateFrom = NULL, @dateTo = NULL";
            }
            else
            {
                query = string.Format("EXEC [dbo].[sp_GetReportSales] @dateFrom = NULL, @dateTo = NULL, @supplierName = '{0}'", supplierName);
            }

            IEnumerable<ReportSales> salestoday = db.Database.SqlQuery<ReportSales>(query);
            ViewBag.reporttoday = salestoday.ToList();

            return View();
        }

        public ActionResult ReportSales(string date1, string date2)
        {
            var supplierName = User.Identity.Name;

            if (date1 != null && date2 != null)
            {
                //string query = "select ProductIns.ProductCode  , SUM (ProductIns.Qty) as ProductIn ,  SUM (ProductOuts.Qty) as ProductOut  ,SUM (ProductIns.Qty) -  SUM (ProductOuts.Qty) as Stock from ProductIns join ProductOuts on ProductIns.ProductCode=ProductIns.ProductCode where ProductIns.Date >= '"+date1+"' AND ProductIns.Date <= '"+date2+"' AND ProductOuts.Date >= '"+date1+"' AND ProductOuts.Date <= '"+date2+"' group by ProductIns.ProductCode ";
                string query = string.Empty;

                if (User.IsInRole("admin"))
                {
                    query = string.Format("EXEC [dbo].[sp_GetReportSales] @dateFrom = '{0}', @dateTo = '{1}'", date1, date2);
                }
                else
                {
                    query = string.Format("EXEC [dbo].[sp_GetReportSales] @dateFrom = '{0}', @dateTo = '{1}', @supplierName = '{2}'", date1, date2, supplierName);
                }

                IEnumerable<ReportSales> reportsales = db.Database.SqlQuery<ReportSales>(query);
                ViewBag.reportsales = reportsales.ToList();
            }

            return PartialView();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportSupplier()
        {
            var LogUser = User.Identity.Name.ToString();

            return View();
        }
    }
}