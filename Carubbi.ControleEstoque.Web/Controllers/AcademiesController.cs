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
    public class AcademiesController : Controller
    {
        private ControleEstoqueEntities db = new ControleEstoqueEntities();

        // GET: Academies
        public ActionResult Index()
        {
            return View(db.Academies.ToList());
        }

        // GET: Academies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academies academies = db.Academies.Find(id);
            if (academies == null)
            {
                return HttpNotFound();
            }
            return View(academies);
        }

        // GET: Academies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Academies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Academies academies)
        {
            if (ModelState.IsValid)
            {
                db.Academies.Add(academies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academies);
        }

        // GET: Academies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academies academies = db.Academies.Find(id);
            if (academies == null)
            {
                return HttpNotFound();
            }
            return View(academies);
        }

        // POST: Academies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Academies academies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academies);
        }

        // GET: Academies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academies academies = db.Academies.Find(id);
            if (academies == null)
            {
                return HttpNotFound();
            }
            return View(academies);
        }

        // POST: Academies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Academies academies = db.Academies.Find(id);
            db.Academies.Remove(academies);
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
