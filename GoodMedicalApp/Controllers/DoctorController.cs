using GoodMedicalApp.BusinessServices.Services;
using GoodMedicalApp.BusinessServices.Services.Implementation;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace GoodMedicalApp.Controllers
{
    public class DoctorController : Controller
    {
        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        private readonly IDoctorService doctorService;

        // GET: Doctor
        public ActionResult Index()
        {
            var doctors = doctorService.GetAll();
            return View(doctors);
        }

        public ActionResult FormDoctor()
        {
            return View();
        }

        [HttpPost]
        public ContentResult CreateDoctor(Doctor doctor)
        {
            doctorService.Add(doctor);
            doctorService.Save();
            return Content("<p>The doctor was created successfully!</p>");
        }
    }
}