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
    public partial class HumenController : Controller
    {
        private AgricultureContext db = new AgricultureContext();

        // GET: Admin/Humen
        public virtual ActionResult Index()
        {
            return View(db.Humans.ToList());
        }

        // GET: Admin/Humen/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Human human = db.Humans.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // GET: Admin/Humen/Create
        public virtual ActionResult Create()
        {
            var model = new Agriculture.DomainClass.Models.Human();
            return View(model);
        }

        // POST: Admin/Humen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "HumanId,FirstName,LastName,NationalCode,MobileNumber")] Human human)
        {
            if (ModelState.IsValid)
            {
                db.Humans.Add(human);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(human);
        }

        // GET: Admin/Humen/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Human human = db.Humans.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // POST: Admin/Humen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "HumanId,FirstName,LastName,NationalCode,MobileNumber")] Human human)
        {
            if (ModelState.IsValid)
            {
                db.Entry(human).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(human);
        }

        // GET: Admin/Humen/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Human human = db.Humans.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // POST: Admin/Humen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            Human human = db.Humans.Find(id);
            db.Humans.Remove(human);
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
