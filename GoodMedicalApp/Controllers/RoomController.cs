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
    public class RoomController : Controller
    {
        public RoomController()
        {
            roomService = new RoomService();
        }

        private readonly IRoomService roomService;

        // GET: Patient
        public ActionResult Index()
        {
            var rooms = roomService.GetAll();
            if (rooms != null && rooms.Any())
            {
                return View(rooms);
            }
            else
            {
                return Content("<p>The list of rooms is empty<p>");
            }
        }

        public ActionResult FormRoom()
        {
            return View();
        }

        [HttpPost]
        public ContentResult CreateRoom(Room room)
        {
            roomService.Add(room);
            roomService.Save();
            return Content("<p>The room was created successfully!</p>");
        }

        public ContentResult Remove(int id)
        {
            var room = roomService.GetItemById(id);
            if (room != null)
            {
                roomService.Remove(room);
                roomService.Save();
                return Content("<p>The room was removed successfully!</p>");
            }
            else
            {
                return Content("<p>The room wasn't found!</p>");
            }
        }

    }
}