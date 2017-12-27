using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodMedicalApp.BusinessServices.Services.Implementation;
using GoodMedicalApp.BusinessServices.Services;
using GoodMedicalApp.Domain.Models;

namespace GoodMedicalApp.Controllers
{
    public class TreatmentController : Controller
    {
        public TreatmentController()
        {
            treatmentService = new TreatmentService();
            doctorService = new DoctorService();
            patientService = new PatientService();
        }

        private readonly ITreatmentService treatmentService;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;

        // GET: Treatment
        public ActionResult Index()
        {
            var treatments = treatmentService.GetAll();
            var doctors = doctorService.GetAll();
            var patients = patientService.GetAll();
            
            ViewBag.doctors = doctors;
            ViewBag.patients = patients;
            return View(treatments);
        }

        public ActionResult FormTreatment()
        {
            var doctors = doctorService.GetAll();
            var patients = patientService.GetAll();

            var doctorsList = new SelectList(doctors, "Id", "FirstName");
            var patientsList = new SelectList(patients, "Id", "FirstName");
            ViewBag.doctors = doctorsList;
            ViewBag.patients = patientsList;
            return View();
        }

        [HttpPost]
        public ContentResult CreateTreatment(Treatment treatment)
        {
            treatmentService.Add(treatment);
            treatmentService.Save();
            return Content("<p>The patient was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var treatment = treatmentService.GetItemById(id);
            if (treatment != null)
            {
                treatmentService.Remove(treatment);
                treatmentService.Save();
                return Content("<p>The treatment was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The treatment wasn't found!</p>");
            }
        }

        public ActionResult FormUpdate(int id)
        {
            var treatment = treatmentService.GetItemById(id);
            if (treatment != null)
            {
                var doctors = doctorService.GetAll();
                var dcotorSelectList = new SelectList(doctors, "Id", "FirstName", "FirstName");
                ViewBag.doctors = dcotorSelectList;

                var patients = patientService.GetAll();
                var patientSelectList = new SelectList(patients, "Id", "FirstName", "FirstName");
                ViewBag.patients = patientSelectList;

                return View(treatment);
            }
            else
            {
                return Content("<p>The treatment wasn't found!</p>");
            }
        }

        [HttpPost]
        public ContentResult Update(Treatment treatment)
        {
            treatmentService.Update(treatment);
            treatmentService.Save();
            return Content("<p>The treatment was updated successfully!</p>");
        }
    }
}