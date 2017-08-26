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
    public class SalesController : Controller
    {
        private ControleEstoqueEntities db = new ControleEstoqueEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Products).Include(s => s.Students);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,ProductId,Price,AcademyId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                if (db.ProductStock.Where(p => p.ProductId == sales.ProductId && p.AcademyId == sales.AcademyId).Count() > 0)
                {
                    sales.SaleDate = DateTime.Now;
                    db.Sales.Add(sales);


                    db.ProductStock.Add(new ProductStock
                    {
                        Direction = false,
                        OperationDate = DateTime.Now,
                        ProductId = sales.ProductId,
                        Quantity = 1,
                        AcademyId = sales.AcademyId,
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sem estoque para este produto");
                }
            }
            
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", sales.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", sales.StudentId);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", sales.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", sales.StudentId);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,ProductId,SaleDate,Price")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademyId = new SelectList(db.Academies, "Id", "Name", sales.AcademyId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", sales.StudentId);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
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
