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
    public class OperationController : Controller
    {
        public OperationController()
        {
            operationService = new OperationService();
            treatmentService = new TreatmentService();
            typeOperationService = new TypeOperationService();
            medicineService = new MedicineService();
        }

        private readonly IOperationService operationService;
        private readonly ITreatmentService treatmentService;
        private readonly ITypeOperationService typeOperationService;
        private readonly IMedicineService medicineService;

        // GET: Operation
        public ActionResult Index()
        {

            var operations = operationService.GetAll();
            return View(operations);
        }

        public ActionResult FormOperation()
        {
            var treatments = treatmentService.GetAll();
            var typeOperations = typeOperationService.GetAll();
            var medicines = medicineService.GetAll();

            var selectListTreatments = new SelectList(treatments, "Id", "Id");
            var selectListTypeOperations = new SelectList(typeOperations, "Id", "Name", "Name");

            ViewBag.treatments = selectListTreatments;
            ViewBag.typeOperations = selectListTypeOperations;
            ViewBag.medicines = medicines; 

            return View(new OperationTransfer());
        }

        public ContentResult CreateOperation(OperationTransfer operationTransfer)
        {           
            var operation = new Operation();
            operation.CurrentTypeOperation = new TypeOperation() { Id = operationTransfer.CurrentTypeOperation.Id};
            operation.TreatmentId = operationTransfer.TreatmentId;
            if(operationTransfer.Medicines.Any())
            {
                operation.Medicines = new List<Medicine>();
                foreach(var id in operationTransfer.Medicines)
                {
                    var medicine = medicineService.GetItemById(id);
                    operation.Medicines.Add(medicine);
                }
            }

            operationService.Add(operation);
            operationService.Save();
            return Content("<p>The operation was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var operation = operationService.GetItemById(id);
            if (operation != null)
            {
                operationService.Remove(operation);
                operationService.Save();
                return Content("<p>The operation was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The operation wasn't found!</p>");
            }

        }

        public ActionResult FormUpdate(int id)
        {
            var operationTransfer = operationService.GetTransferItemById(id);
            if (operationTransfer != null)
            {
                var treatments = treatmentService.GetAll();
                var typeOperations = typeOperationService.GetAll();
                var medicines = medicineService.GetAll();

                var selectListTreatments = new SelectList(treatments, "Id", "Id");
                var selectListTypeOperations = new SelectList(typeOperations, "Id", "Name", "Name");

                ViewBag.treatments = selectListTreatments;
                ViewBag.typeOperations = selectListTypeOperations;
                ViewBag.medicines = medicines;

                return View(operationTransfer);
            }
            else
            {
                return Content("<p>The operation wasn't found!</p>");
            }
        }

        [HttpPost]
        public ContentResult Update(OperationTransfer operationTransfer)
        {
            var operation = operationService.GetItemFromTransferItem(operationTransfer);
            if(operation != null)
            {
                operationService.Update(operation);
                operationService.Save();
                return Content("<p>The operation was updated successfully!</p>");
            }
            else
            {
                return Content("<p>The operation wasn't updated! The transfer object wasn't found</p>");
            }
        }

    }
}