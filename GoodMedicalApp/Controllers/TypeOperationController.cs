﻿using GoodMedicalApp.BusinessServices.Services;
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
            return Content("<p>The room was created successfully!</p>");
        }
    }
}