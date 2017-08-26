using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Carubbi.ControleEstoque.Web.Models;

namespace Carubbi.ControleEstoque.Web.Controllers
{
    public class ProductStocksController : Controller
    {
        private ControleEstoqueEntities db = new ControleEstoqueEntities();

        // GET: ProductStocks
        public ActionResult Index()
        {
            var productStock = db.ProductStock.Include(p => p.Academies).Include(p => p.Products);
            return View(productStock.ToList());
        }

        // GET: ProductStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStock productStock = db.ProductStock.Find(id);
            if (productStock == null)
            {
                return HttpNotFound();
            }
            return View(productStock);
        }

        // GET: ProductStocks/Create
        public ActionResult Create()
        {
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: ProductStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quantity,ProductId,AcademyId")] ProductStock productStock)
        {
            if (ModelState.IsValid)
            {
                productStock.OperationDate = DateTime.Now;
                productStock.Direction = true;
                db.ProductStock.Add(productStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", productStock.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productStock.ProductId);
            return View(productStock);
        }

        // GET: ProductStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStock productStock = db.ProductStock.Find(id);
            if (productStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", productStock.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productStock.ProductId);
            return View(productStock);
        }

        // POST: ProductStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,OperationDate,Direction,ProductId,AcademyId")] ProductStock productStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", productStock.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productStock.ProductId);
            return View(productStock);
        }

        // GET: ProductStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStock productStock = db.ProductStock.Find(id);
            if (productStock == null)
            {
                return HttpNotFound();
            }
            return View(productStock);
        }

        // POST: ProductStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductStock productStock = db.ProductStock.Find(id);
            db.ProductStock.Remove(productStock);
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
