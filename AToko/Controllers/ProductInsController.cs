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
    public class ProductInsController : Controller
    {
        private ATokoDb db = new ATokoDb();

        // GET: ProductIns
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var producsIn = db.ProducsIn.Include(p => p.Product);
            return View(producsIn.ToList());
        }

        // GET: ProductIns/Details/5
        //[Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIn productIn = db.ProducsIn.Find(id);
            if (productIn == null)
            {
                return HttpNotFound();
            }
            return View(productIn);
        }

        // GET: ProductIns/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
          ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "ProductCode");
            return View();
        }

        // POST: ProductIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductInID,ProductCode,Date,Qty,Notes")] ProductIn productIn)
        {
            productIn.Date = System.DateTime.Today;
            var price = db.Products.Where(o => o.ProductCode == productIn.ProductCode).Select(o => o.Price).FirstOrDefault();
            productIn.Price = productIn.Qty * price;
            if (ModelState.IsValid)
            {
                db.ProducsIn.Add(productIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductCode", "ProductCode", productIn.ProductCode);
            return View(productIn);
        }

        // GET: ProductIns/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIn productIn = db.ProducsIn.Find(id);
            if (productIn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductCode", "ProductCode", productIn.ProductCode);
            return View(productIn);
        }

        // POST: ProductIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductInID,ProductCode,Qty,Price,Notes")] ProductIn productIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductCode", "ProductCode", productIn.ProductCode);
            return View(productIn);
        }

        // GET: ProductIns/Delete/5
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIn productIn = db.ProducsIn.Find(id);
            if (productIn == null)
            {
                return HttpNotFound();
            }
            return View(productIn);
        }

        // POST: ProductIns/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductIn productIn = db.ProducsIn.Find(id);
            db.ProducsIn.Remove(productIn);
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
