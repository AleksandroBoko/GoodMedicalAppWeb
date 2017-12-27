using GoodMedicalApp.BusinessServices.Services;
using GoodMedicalApp.BusinessServices.Services.Implementation;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodMedicalApp.Controllers
{
    public class PatientController : Controller
    {
        public PatientController()
        {
            patientService = new PatientService();
            roomService = new RoomService();
        }

        private readonly IPatientService patientService;
        private readonly IRoomService roomService;

        // GET: Patient
        public ActionResult Index()
        {
            var patients = patientService.GetAll();
            var rooms = roomService.GetAll();            
            ViewBag.rooms = rooms;
            
            return View(patients);
        }

        public ActionResult FormPatient()
        {
            
            var rooms = roomService.GetAll();
            var selectList = new SelectList(rooms, "Id", "Number", "Number");
            ViewBag.selectList = selectList;

            return View();
        }

        [HttpPost]
        public ContentResult CreatePatient(Patient patient)
        {
            patientService.Add(patient);
            patientService.Save();
            return Content("<p>The patient was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var patient = patientService.GetItemById(id);
            if (patient != null)
            {
                patientService.Remove(patient);
                patientService.Save();
                return Content("<p>The patient was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The patient wasn't found!</p>");
            }
        }

        public ActionResult FormUpdate(int id)
        {
            var patient = patientService.GetItemById(id);
            if (patient != null)
            {
                var rooms = roomService.GetAll();
                var selectList = new SelectList(rooms, "Id", "Number", "Number");
                ViewBag.selectList = selectList;

                return View(patient);
            }
            else
            {
                return Content("<p>The patient wasn't found!</p>");
            }
        }

        [HttpPost]
        public ContentResult Update(Patient patient)
        {
            patientService.Update(patient);
            patientService.Save();
            return Content("<p>The patient was updated successfully!</p>");
        }
    }
}