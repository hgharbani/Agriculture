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
using Agriculture.Model.ViewModel;

namespace Agriculture.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public partial class AreaMapsController : Controller
    {
        private AgricultureContext db = new AgricultureContext();

        // GET: Admin/AreaMaps
        public virtual ActionResult Index()
        {
            return View(db.AreaMaps.ToList());

        }

        [HttpPost]
        [Global.StructureMapDependencyResolver.AjaxOnly]
        public virtual ActionResult GetMapPoint()
        {
            var mappointQuery = db.AreaMaps.Select(a => new
            {
                AreaName = a.AreaName,
                color=a.MapAreaType.Color,
                AreaType=a.AreaTypeId,
                mapPoints = a.MapPoints.Select(b=>new
                {
                    lat=b.LatMap,
                    lng=b.LngMap
                }).ToList()
            }).ToList();
            return Json(mappointQuery);
        }
        // GET: Admin/AreaMaps/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaMap areaMap = db.AreaMaps.Find(id);
            if (areaMap == null)
            {
                return HttpNotFound();
            }
            return View(areaMap);
        }

        // GET: Admin/AreaMaps/Create
        public virtual ActionResult Create()
        {
            var model=new AddOrUpdateAreaMap();
            var areatype = db.MapAreaTypes.ToList();
            foreach (var mapAreaType in areatype)
            {
                model.AvilableAreaTypes.Add(mapAreaType);
            }
            return View(model);
        }

        // POST: Admin/AreaMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdateAreaMap areaMapModel)
        {
            if (ModelState.IsValid)
            {
                var areamap=new AreaMap()
                {
                    AreaName = areaMapModel.AreaName,
                    AreaTypeId = areaMapModel.AreaTypeId,
                    MapPoints=new List<MapPoint>()
                };
                foreach (var areamapMapPoint in areaMapModel.MapPoints)
                {
                    if (db.MapPoints.Any(a =>
                        a.LatMap.CompareTo(areamapMapPoint.LatMap)==0 || a.LngMap.CompareTo(areamapMapPoint.LngMap)==0))
                    {

                        return Json("محیط انتخاب شده تداخل دارد");
                    }
                    var mapPoint=new MapPoint()
                    {
                        LatMap = areamapMapPoint.LatMap,
                        LngMap = areamapMapPoint.LngMap
                    };
                    areamap.MapPoints.Add(mapPoint);
                }

                db.AreaMaps.Add(areamap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(areaMapModel);
        }

        // GET: Admin/AreaMaps/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaMap areaMap = db.AreaMaps.Find(id);
            if (areaMap == null)
            {
                return HttpNotFound();
            }
            return View(areaMap);
        }

        // POST: Admin/AreaMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "AreaId,AreaName,LatMap,LngMap,AreaTypeId")] AreaMap areaMap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaMap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaMap);
        }

        // GET: Admin/AreaMaps/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaMap areaMap = db.AreaMaps.Find(id);
            if (areaMap == null)
            {
                return HttpNotFound();
            }
            return View(areaMap);
        }

        // POST: Admin/AreaMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            AreaMap areaMap = db.AreaMaps.Find(id);
            db.AreaMaps.Remove(areaMap);
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
