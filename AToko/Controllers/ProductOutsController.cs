using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AToko.DataContexts;
using ATokoEntities;

namespace AToko.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductOutsController : Controller
    {
        private ATokoDb db = new ATokoDb();

        // GET: ProductOuts
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var productsOut = db.ProductsOut.Include(p => p.Product);
            return View(productsOut.ToList());
        }

        // GET: ProductOuts/Details/5
        //[Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductsOut.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            return View(productOut);
        }

        // GET: ProductOuts/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "ProductCode");
            return View();
        }

        // POST: ProductOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductOutID,Date,ProductCode,Qty,Notes")] ProductOut productOut)
        {

            productOut.Date = System.DateTime.Today;
            if (ModelState.IsValid)
            {
                db.ProductsOut.Add(productOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "ProductCode", productOut.ProductCode);
            return View(productOut);
        }

        // GET: ProductOuts/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductsOut.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "ProductCode", productOut.ProductCode);
            return View(productOut);
        }

        // POST: ProductOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductOutID,ProductCode,Qty,Notes")] ProductOut productOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductCode", "ProductCode", productOut.ProductCode);
            return View(productOut);
        }

        // GET: ProductOuts/Delete/5
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductsOut.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            return View(productOut);
        }

        // POST: ProductOuts/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductOut productOut = db.ProductsOut.Find(id);
            db.ProductsOut.Remove(productOut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
