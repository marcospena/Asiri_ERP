﻿using System;
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
    public class CLlt03_atencionController : Controller
    {
        private Asiri_ERPEntities db = new Asiri_ERPEntities();

        // GET: CLlt03_atencion
        public ActionResult Index()
        {
            var cLlt03_atencion = db.CLlt03_atencion.Include(c => c.CLlt05_cita);
            return View(cLlt03_atencion.ToList());
        }

        // GET: CLlt03_atencion/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt03_atencion cLlt03_atencion = db.CLlt03_atencion.Find(id);
            if (cLlt03_atencion == null)
            {
                return HttpNotFound();
            }
            return View(cLlt03_atencion);
        }

        public string Codigo()
        {
            Random obj = new Random();
            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 5;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }

        // GET: CLlt03_atencion/Create
        public ActionResult Create(long? id)
        {
            //Calcular Codigo Atencion
            CLlt03_atencion oCLlt03_atencion = new CLlt03_atencion();
            for (int i = 0; i >= 0; i++)
            {
                string nuevacadena = Codigo();
                var lista = db.CLlt03_atencion.Where(x => x.codAtencion == nuevacadena).ToList();
                if (lista.Count == 0)
                {
                    oCLlt03_atencion.codAtencion = nuevacadena;
                    break;

                }
                i++;
            }
            oCLlt03_atencion.idCita = id;
            var cita = db.CLlt05_cita.Find(id);
            //Informacion basica
            ViewBag.paciente = cita.RHUt07_paciente.RHUt09_persona.apellidoPaterno + " " + cita.RHUt07_paciente.RHUt09_persona.apellidoMaterno + ", " + cita.RHUt07_paciente.RHUt09_persona.nombrePersona;
            ViewBag.numDocumento = cita.RHUt07_paciente.RHUt09_persona.numDocIdentidad;
            ViewBag.numHistoria = cita.RHUt07_paciente.numHistoriaClinica;
            ViewBag.idCie10 = new SelectList(db.CLlt04_cie10, "idCie10", "descCie10");
            return View(oCLlt03_atencion);
        }

        // POST: CLlt03_atencion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateAtencion(CLlt03_atencion cLlt03_atencion)
        {
            //Atencion
            cLlt03_atencion.idUsuario = 1;
            cLlt03_atencion.fecRegistro = DateTime.Now;
            db.CLlt03_atencion.Add(cLlt03_atencion);
            db.SaveChanges();
            var ultimaAtencion = db.CLlt03_atencion.OrderByDescending(x => x.idAtencion).Take(1).ToList();
            
            //Diagnostico y Tratamiento
            CLlt08_diagnostico oCLlt08_diagnostico = new CLlt08_diagnostico();
            CLlt16_tratamiento oCLlt16_tratamiento = new CLlt16_tratamiento();
            for (int i = 0; i < cLlt03_atencion.oListDiagnostico.Count; i++)
            {
                oCLlt08_diagnostico.idCie10 = cLlt03_atencion.oListDiagnostico[i].idCie10;
                oCLlt08_diagnostico.descDiagnostico = cLlt03_atencion.oListDiagnostico[i].descDiagnostico;
                oCLlt08_diagnostico.idAtencion = ultimaAtencion[0].idAtencion;
                db.CLlt08_diagnostico.Add(oCLlt08_diagnostico);
                db.SaveChanges();

                var ultimoDiagnostico = db.CLlt08_diagnostico.OrderByDescending(x => x.idDiagnostico).Take(1).ToList();

                oCLlt16_tratamiento.descTratamiento = cLlt03_atencion.oListTratamiento[i].descTratamiento;
                oCLlt16_tratamiento.idDiagnostico = ultimoDiagnostico[0].idDiagnostico;
                db.CLlt16_tratamiento.Add(oCLlt16_tratamiento);
                db.SaveChanges();

            }

            //Anamnesis
            cLlt03_atencion.oAnamnesis.idAtencion = ultimaAtencion[0].idAtencion;
            db.CLIt01_anamnesis.Add(cLlt03_atencion.oAnamnesis);
            db.SaveChanges();

            //Evolucion
            CLlt11_evolucion oCLlt11_evolucion = new CLlt11_evolucion();
            for (int i = 0; i < cLlt03_atencion.oListEvolucion.Count; i++)
            {
                oCLlt11_evolucion.idAtencion = ultimaAtencion[0].idAtencion;
                oCLlt11_evolucion.descEvolucion = cLlt03_atencion.oListEvolucion[i].descEvolucion;
                db.CLlt11_evolucion.Add(oCLlt11_evolucion);
                db.SaveChanges();
            }

            //Estudio Complementario
            CLlt10_estudioCompl oCLlt10_estudioCompl = new CLlt10_estudioCompl();

            for (int i = 0; i < cLlt03_atencion.oListEstudioCompl.Count; i++)
            {
                oCLlt10_estudioCompl.idAtencion = ultimaAtencion[0].idAtencion;
                oCLlt10_estudioCompl.descEstudioCompl = cLlt03_atencion.oListEstudioCompl[i].descEstudioCompl;
                db.CLlt10_estudioCompl.Add(oCLlt10_estudioCompl);
                db.SaveChanges();
            }


            //Funcion Vital
            cLlt03_atencion.oFuncionVital.idAtencion = ultimaAtencion[0].idAtencion;
            db.CLlt13_funcionVital.Add(cLlt03_atencion.oFuncionVital);
            db.SaveChanges();

            //Examen Fisico
            CLlt12_examenFisico oCLlt12_examenFisico = new CLlt12_examenFisico();
            for (int i = 0; i < cLlt03_atencion.oListExamenFisico.Count; i++)
            {
                oCLlt12_examenFisico.idAtencion = ultimaAtencion[0].idAtencion;
                oCLlt12_examenFisico.descExamenFisico = cLlt03_atencion.oListExamenFisico[i].descExamenFisico;
                db.CLlt12_examenFisico.Add(oCLlt12_examenFisico);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        // GET: CLlt03_atencion/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt03_atencion cLlt03_atencion = db.CLlt03_atencion.Find(id);
            if (cLlt03_atencion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCita = new SelectList(db.CLlt05_cita, "idCita", "codCita", cLlt03_atencion.idCita);
            return View(cLlt03_atencion);
        }

        // POST: CLlt03_atencion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAtencion,codAtencion,fecRegistro,idUsuario,idCita")] CLlt03_atencion cLlt03_atencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLlt03_atencion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCita = new SelectList(db.CLlt05_cita, "idCita", "codCita", cLlt03_atencion.idCita);
            return View(cLlt03_atencion);
        }

        // GET: CLlt03_atencion/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt03_atencion cLlt03_atencion = db.CLlt03_atencion.Find(id);
            if (cLlt03_atencion == null)
            {
                return HttpNotFound();
            }
            return View(cLlt03_atencion);
        }

        // POST: CLlt03_atencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CLlt03_atencion cLlt03_atencion = db.CLlt03_atencion.Find(id);
            db.CLlt03_atencion.Remove(cLlt03_atencion);
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