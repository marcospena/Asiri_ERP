using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_BusinessEntity;

namespace Asiri_ERP.Controllers
{
    public class RHUt13_tipoEspecialidadController : Controller
    {
        private Asiri_ERPEntities db = new Asiri_ERPEntities();

        // GET: RHUt13_tipoEspecialidad
        public ActionResult Index()
        {
            return View(db.RHUt13_tipoEspecialidad.ToList());
        }

        // GET: RHUt13_tipoEspecialidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad = db.RHUt13_tipoEspecialidad.Find(id);
            if (rHUt13_tipoEspecialidad == null)
            {
                return HttpNotFound();
            }
            return View(rHUt13_tipoEspecialidad);
        }

        // GET: RHUt13_tipoEspecialidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RHUt13_tipoEspecialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoDeEspeciliadad,nombreEspecialidad,descEspecialidad,abrvTipoEspecialidad,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
                db.RHUt13_tipoEspecialidad.Add(rHUt13_tipoEspecialidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rHUt13_tipoEspecialidad);
        }

        // GET: RHUt13_tipoEspecialidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad = db.RHUt13_tipoEspecialidad.Find(id);
            if (rHUt13_tipoEspecialidad == null)
            {
                return HttpNotFound();
            }
            return View(rHUt13_tipoEspecialidad);
        }

        // POST: RHUt13_tipoEspecialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoDeEspeciliadad,nombreEspecialidad,descEspecialidad,abrvTipoEspecialidad,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rHUt13_tipoEspecialidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rHUt13_tipoEspecialidad);
        }

        // GET: RHUt13_tipoEspecialidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad = db.RHUt13_tipoEspecialidad.Find(id);
            if (rHUt13_tipoEspecialidad == null)
            {
                return HttpNotFound();
            }
            return View(rHUt13_tipoEspecialidad);
        }

        // POST: RHUt13_tipoEspecialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RHUt13_tipoEspecialidad rHUt13_tipoEspecialidad = db.RHUt13_tipoEspecialidad.Find(id);
            db.RHUt13_tipoEspecialidad.Remove(rHUt13_tipoEspecialidad);
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
