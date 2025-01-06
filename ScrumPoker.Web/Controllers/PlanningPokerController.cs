﻿using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models;
using ScrumPoker.Web.TempData;
using ScrumPoker.Business.Abstract;

namespace ScrumPoker.Web.Controllers
{   
    [Route("planning-poker")]
    public class PlanningPokerController : Controller
    {
        private readonly IPlanningPokerVotingTypeService _planningPokerVotingTypeService;

        public PlanningPokerController(IPlanningPokerVotingTypeService planningPokerVotingTypeService)
        {
            _planningPokerVotingTypeService = planningPokerVotingTypeService;
        }

        [Route("list", Name = "PlanningPokerIndex")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Route("create", Name = "PlanningPokerCreate")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create", Name = "PlanningPokerCreate")]
        public IActionResult Create(PlanningPokerRoomCreateModel model)
        {
            StaticData.RoomList.Add(model);

            return RedirectToAction("Room", new { roomId = model.Id });
        }

        [Route("room/{roomId}", Name = "PlanningPokerRoom")]
        public IActionResult Room(string roomId)
        {
            PlanningPokerIndexViewModel vm = new PlanningPokerIndexViewModel();
            vm.RoomId = roomId;
            vm.Name = StaticData.RoomList.FirstOrDefault(w => w.Id == roomId).Name;

            return View(vm);
        }
        [Route("room-security/{roomId}", Name = "PlanningPokerRoomSecurity")]
        public IActionResult RoomSecurity(string roomId)
        {
            return View();
        }
    }
}
