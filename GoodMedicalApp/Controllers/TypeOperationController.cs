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
    public class TypeOperationController : Controller
    {
        public TypeOperationController()
        {
            typeOperationService = new TypeOperationService();
        }

        private readonly ITypeOperationService typeOperationService;

        // GET: TypeOperation
        public ActionResult Index()
        {
            var typeOperations = typeOperationService.GetAll();
            return View(typeOperations);
        }

        public ActionResult FormTypeOperation()
        {
            return View();
        }

        [HttpPost]
        public ContentResult CreateTypeOperation(TypeOperation typeOperation)
        {
            typeOperationService.Add(typeOperation);
            typeOperationService.Save();
            return Content("<p>The operation's type was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var typeOperation = typeOperationService.GetItemById(id);
            if (typeOperation != null)
            {
                typeOperationService.Remove(typeOperation);
                typeOperationService.Save();
                return Content("<p>The operation's type was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The operation's type wasn't found!</p>");
            }
        }

        public ActionResult FormUpdate(int id)
        {
            var typeOperation = typeOperationService.GetItemById(id);
            return typeOperation != null ? View(typeOperation) as ActionResult :
                                           Content("<p>The operation's type wasn't found!</p>") as ActionResult;
        }

        [HttpPost]
        public ContentResult Update(TypeOperation typeOperation)
        {
            typeOperationService.Update(typeOperation);
            typeOperationService.Save();
            return Content("<p>The operation's type was created successfully!</p>");
        }
    }
}