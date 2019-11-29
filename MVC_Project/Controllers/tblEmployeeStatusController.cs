using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class tblEmployeeStatusController : Controller
    {
        private EmployeeEntities1 db = new EmployeeEntities1();
        [Authorize]
        // GET: tblEmployeeStatus
        public ActionResult Index()
        {
            return View(db.tblEmployeeStatus.ToList());
        }

        // GET: tblEmployeeStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeStatu tblEmployeeStatu = db.tblEmployeeStatus.Find(id);
            if (tblEmployeeStatu == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployeeStatu);
        }

        // GET: tblEmployeeStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEmployeeStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusId,StatusName,Role,Shift")] tblEmployeeStatu tblEmployeeStatu)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployeeStatus.Add(tblEmployeeStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEmployeeStatu);
        }

        // GET: tblEmployeeStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeStatu tblEmployeeStatu = db.tblEmployeeStatus.Find(id);
            if (tblEmployeeStatu == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployeeStatu);
        }

        // POST: tblEmployeeStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusId,StatusName,Role,Shift")] tblEmployeeStatu tblEmployeeStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployeeStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEmployeeStatu);
        }

        // GET: tblEmployeeStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeStatu tblEmployeeStatu = db.tblEmployeeStatus.Find(id);
            if (tblEmployeeStatu == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployeeStatu);
        }

        // POST: tblEmployeeStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmployeeStatu tblEmployeeStatu = db.tblEmployeeStatus.Find(id);
            db.tblEmployeeStatus.Remove(tblEmployeeStatu);
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
