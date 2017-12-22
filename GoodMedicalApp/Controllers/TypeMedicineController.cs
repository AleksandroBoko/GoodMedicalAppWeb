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
    public class TypeMedicineController : Controller
    {
        public TypeMedicineController()
        {
            typeMedicineService = new TypeMedicineService();
        }

        private readonly ITypeMedicineService typeMedicineService;

        // GET: TypeMedicine
        public ActionResult Index()
        {
            var typeMedicines = typeMedicineService.GetAll();
            return View(typeMedicines);
        }

        public ActionResult FormTypeMedicine()
        {
            return View();
        }

        [HttpPost]
        public ContentResult CreateTypeMedicine(TypeMedicine typeMedicine)
        {
            typeMedicineService.Add(typeMedicine);
            typeMedicineService.Save();
            return Content("<p>The medicine's type was created successfully!</p>");
        }
    }
}