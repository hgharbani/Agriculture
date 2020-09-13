using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agriculture.Data;
using Agriculture.DomainClass.Models;

namespace Agriculture.Areas.Admin.Controllers
{
    [Authorize]
    public partial class MapAreaTypesController : Controller
    {
        private AgricultureContext db = new AgricultureContext();

        // GET: Admin/MapAreaTypes
        public virtual ActionResult Index()
        {
            return View(db.MapAreaTypes.ToList());
        }

        // GET: Admin/MapAreaTypes/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapAreaType mapAreaType = db.MapAreaTypes.Find(id);
            if (mapAreaType == null)
            {
                return HttpNotFound();
            }
            return View(mapAreaType);
        }

        // GET: Admin/MapAreaTypes/Create
        public virtual ActionResult Create()
        {
            var model = new MapAreaType();
            return View(model);
        }

        // POST: Admin/MapAreaTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,TypeName,Color")] MapAreaType mapAreaType)
        {
            if (ModelState.IsValid)
            {
                db.MapAreaTypes.Add(mapAreaType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mapAreaType);
        }

        // GET: Admin/MapAreaTypes/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapAreaType mapAreaType = db.MapAreaTypes.Find(id);
            if (mapAreaType == null)
            {
                return HttpNotFound();
            }
            return View(mapAreaType);
        }

        // POST: Admin/MapAreaTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,TypeName,Color")] MapAreaType mapAreaType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mapAreaType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mapAreaType);
        }

        // GET: Admin/MapAreaTypes/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapAreaType mapAreaType = db.MapAreaTypes.Find(id);
            if (mapAreaType == null)
            {
                return HttpNotFound();
            }
            return View(mapAreaType);
        }

        // POST: Admin/MapAreaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            MapAreaType mapAreaType = db.MapAreaTypes.Find(id);
            db.MapAreaTypes.Remove(mapAreaType);
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
