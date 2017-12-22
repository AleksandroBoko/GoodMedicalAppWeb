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
    public class TreatmentReportController : Controller
    {
        public TreatmentReportController()
        {
            treatmentReportService = new TreatmentReportService();
            treatmentService = new TreatmentService();
            medicineService = new MedicineService();
        }

        private readonly ITreatmentReportService treatmentReportService;
        private readonly ITreatmentService treatmentService;
        private readonly IMedicineService medicineService;

        // GET: TreatmentReport
        public ActionResult Index()
        {
            var treatmentReports = treatmentReportService.GetAll();
            return View(treatmentReports);
        }

        public ActionResult FormTreatmentReport()
        {
            var treatments = treatmentService.GetAll();            
            var medicines = medicineService.GetAll();

            var selectListTreatments = new SelectList(treatments, "Id", "Id");   

            ViewBag.treatments = selectListTreatments;            
            ViewBag.medicines = medicines;

            return View();
        }

        public ContentResult CreateTreatmentReport(TreatmentReportTransfer treatmentReportTransfer)
        {
            var treatmentReport = new TreatmentReport();
            treatmentReport.Conclusion = treatmentReportTransfer.Conclusion;
            treatmentReport.Comment = treatmentReportTransfer.Comment;
            treatmentReport.CurrentTreatment = new Treatment() { Id = treatmentReportTransfer.CurrentTreatment.Id};

            if (treatmentReportTransfer.Medicines.Any())
            {
                treatmentReport.Medicines = new List<Medicine>();
                foreach (var id in treatmentReportTransfer.Medicines)
                {
                    var medicine = new Medicine() { Id = id };
                    treatmentReport.Medicines.Add(medicine);
                }
            }

            treatmentReportService.Add(treatmentReport);
            treatmentReportService.Save();
            return Content("<p>The treatment's report was created successfully!</p>");
        }
    }
}