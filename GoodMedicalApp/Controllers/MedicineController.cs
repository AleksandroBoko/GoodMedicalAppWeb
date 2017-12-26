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
    public class MedicineController : Controller
    {
        public MedicineController()
        {
            medicineService = new MedicineService();
            typeMedicineService = new TypeMedicineService();
        }

        private readonly IMedicineService medicineService;
        private readonly ITypeMedicineService typeMedicineService; 

        // GET: Medicine
        public ActionResult Index()
        {
            var medicines = medicineService.GetAll();                       
            return View(medicines);
        }

        public ActionResult FormMedicine()
        {
            var typeMedicines = typeMedicineService.GetAll();
            var selectList = new SelectList(typeMedicines, "Id", "Name", "Name");
            ViewBag.selectList = selectList;

            return View();
        }

        [HttpPost]
        public ContentResult CreateMedicine(Medicine medicine)
        {
            medicineService.Add(medicine);
            medicineService.Save();
            return Content("<p>The medicine was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var medicine = medicineService.GetItemById(id);
            if (medicine != null)
            {
                medicineService.Remove(medicine);
                medicineService.Save();
                return Content("<p>The medicine was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The medicine wasn't found!</p>");
            }
        }
    }
}