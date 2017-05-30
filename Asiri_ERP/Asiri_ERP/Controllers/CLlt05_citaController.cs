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
    public class CLlt05_citaController : Controller
    {
        private Asiri_ERPEntities db = new Asiri_ERPEntities();

        // GET: CLlt05_cita
        public ActionResult Index()
        {
            var cLlt05_cita = db.CLlt05_cita.Include(c => c.CLlt09_estadoCita).Include(c => c.RHUt01_empleado).Include(c => c.RHUt07_paciente);
            return View(cLlt05_cita.ToList());
        }

        //Busca el paciente por numero documento
        public ActionResult BuscarPacientePorND(string numeroDocumento)
        {
            var ListaPaciente = db.RHUt09_persona.Where(x => x.numDocIdentidad == numeroDocumento).ToList();
            return View();
        }
        // GET: CLlt05_cita/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt05_cita cLlt05_cita = db.CLlt05_cita.Find(id);
            if (cLlt05_cita == null)
            {
                return HttpNotFound();
            }
            return View(cLlt05_cita);
        }

        public ActionResult Calendario()
        {
            return View();
        }

        // GET: CLlt05_cita/Create
        public ActionResult Create()
        {
            ViewBag.idEstadoCita = new SelectList(db.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita");
            ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado, "idEmpleado", "codEmpleado");
            ViewBag.idPaciente = new SelectList(db.RHUt07_paciente, "idPaciente", "codPaciente");
            ViewBag.idServicio = new SelectList(db.PROt04_servicio, "idServicio", "nombreServicio");
            var ListaServicio = db.PROt04_servicio.ToList();
            ViewBag.precio = ListaServicio[0].precio.ToString();
            return View();
        }

        //Obtiene el servicio y manda el precio
        [HttpGet]
        public ActionResult ObtenerServicio(int idServicio)
        {
            var ListaServicio = db.PROt04_servicio.Where(x => x.idServicio == idServicio).ToList();
            ViewBag.precio = ListaServicio[0].precio.ToString();
            return Json(ListaServicio[0].precio.ToString(), JsonRequestBehavior.AllowGet);
        }

        //Crear Cita Medica
        [HttpPost]
        public JsonResult Create(CLlt05_cita oCita)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    oCita.duracionEstimada = "10";
                    oCita.fecRegistro = DateTime.Now;
                    oCita.numReprogramacion = 1;
                    oCita.esCerrado = false;
                    oCita.idUsuario = 1;
                    oCita.idEstadoCita = 1;
                    db.CLlt05_cita.Add(oCita);
                    db.SaveChanges();
                    List<CLlt05_cita> CitaCabecera = new List<CLlt05_cita>();
                    CitaCabecera = db.CLlt05_cita.OrderByDescending(x => x.idCita).Take(1).ToList();
                    CLlt06_citaDtl oCitaDtl = new CLlt06_citaDtl();
                    for (int i = 0; i < oCita.oListCitaDtl.Count; i++)
                    {
                        //oCitaDtl.fecRegistro = DateTime.Now;
                        oCitaDtl.fecModificacion = oCita.oListCitaDtl[i].fecModificacion;
                        oCitaDtl.fecEliminacion = oCita.oListCitaDtl[i].fecEliminacion;
                        oCitaDtl.precio = oCita.oListCitaDtl[i].precio;
                        oCitaDtl.cantidad = oCita.oListCitaDtl[i].cantidad;
                        oCitaDtl.esGratis = true;
                        oCitaDtl.activo = true;
                        oCitaDtl.idCita = CitaCabecera[0].idCita;
                        oCitaDtl.idServicio = oCita.oListCitaDtl[i].idServicio;
                        //oCitaDtl.idUsuario = 1;
                        oCitaDtl.idUsuarioModificar = oCita.oListCitaDtl[i].idUsuarioModificar;
                        oCitaDtl.idUsuarioEliminar = oCita.oListCitaDtl[i].idUsuarioEliminar;

                        db.CLlt06_citaDtl.Add(oCitaDtl);
                        db.SaveChanges();
                    }

                    return Json(new { Success = 1, resultado = true, ex = "" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }

        // GET: CLlt05_cita/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt05_cita cLlt05_cita = db.CLlt05_cita.Find(id);
            if (cLlt05_cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstadoCita = new SelectList(db.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita", cLlt05_cita.idEstadoCita);
            ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado, "idEmpleado", "codEmpleado", cLlt05_cita.idEmpleado);
            ViewBag.idPaciente = new SelectList(db.RHUt07_paciente, "idPaciente", "codPaciente", cLlt05_cita.idPaciente);
            return View(cLlt05_cita);
        }

        // POST: CLlt05_cita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCita,codCita,fecCita,fecRegistro,fecModificacion,horaInicio,duracionEstimada,numReprogramacion,esOnline,esCerrado,idCitaPadre,obsvCita,mtoTotal,idEstadoCita,idPaciente,idEmpleado,idUsuario,idUsuarioModificar")] CLlt05_cita cLlt05_cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLlt05_cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstadoCita = new SelectList(db.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita", cLlt05_cita.idEstadoCita);
            ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado, "idEmpleado", "codEmpleado", cLlt05_cita.idEmpleado);
            ViewBag.idPaciente = new SelectList(db.RHUt07_paciente, "idPaciente", "codPaciente", cLlt05_cita.idPaciente);
            return View(cLlt05_cita);
        }

        // GET: CLlt05_cita/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt05_cita cLlt05_cita = db.CLlt05_cita.Find(id);
            if (cLlt05_cita == null)
            {
                return HttpNotFound();
            }
            return View(cLlt05_cita);
        }

        // POST: CLlt05_cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CLlt05_cita cLlt05_cita = db.CLlt05_cita.Find(id);
            db.CLlt05_cita.Remove(cLlt05_cita);
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
