using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Code_Project.Models;

namespace Code_Project.Controllers
{
    public class PatientInfoController : Controller
    {
        private Code_ProjectEntities6 db = new Code_ProjectEntities6();

        // GET: PatientInfo
        public ActionResult Index()
        {
            return View(db.Patient_Info.ToList());
        }

        // GET: PatientInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Info patientInfo = db.Patient_Info.Find(id);
            if (patientInfo == null)
            {
                return HttpNotFound();
            }
            return View(patientInfo);
        }

        // GET: PatientInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth,IdNumber,Medical_Aid")] Patient_Info patientInfo)
        {
            if (!string.IsNullOrWhiteSpace(patientInfo.DateOfBirth.ToString()) && (!string.IsNullOrWhiteSpace(patientInfo.IdNumber)))
            {
                var dateOfBirth = patientInfo.DateOfBirth.ToString();
                var year = dateOfBirth.Substring(8, 2);
                var month = dateOfBirth.Substring(3, 2);
                var day = dateOfBirth.Substring(0, 2);
                var firstSixNumDOB = year + month + day;
                var firstSixNumID = patientInfo.IdNumber.Substring(0, 6);

                bool result = firstSixNumDOB == firstSixNumID;
                if (!result)
                {
                    ModelState.AddModelError(nameof(patientInfo.IdNumber), "ID Number does not match date of birth");
                    return View(patientInfo);
                }

                if (ModelState.IsValid)
                {
                    db.Patient_Info.Add(patientInfo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(patientInfo);
        }

        // GET: PatientInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Info patientInfo = db.Patient_Info.Find(id);
            if (patientInfo == null)
            {
                return HttpNotFound();
            }
            return View(patientInfo);
        }

        // POST: PatientInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth,IdNumber,Medical_Aid")] Patient_Info patientInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientInfo);
        }

        // GET: PatientInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Info patientInfo = db.Patient_Info.Find(id);
            if (patientInfo == null)
            {
                return HttpNotFound();
            }
            return View(patientInfo);
        }

        // POST: PatientInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient_Info patientInfo = db.Patient_Info.Find(id);
            db.Patient_Info.Remove(patientInfo);
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