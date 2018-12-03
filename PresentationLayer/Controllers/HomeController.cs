using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using EntitiesLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using BusinessLogicLayer.Service;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            using (GameContext db = new GameContext())
            {
                var service = new GameService(new GameRepository(db));
                //await service.CreateNewGame(new User { UserRole = UserRole.PeoplePlayer, Nickname = "ass228" }, 3);
                //await service.CreateNewRound(1);
                await service.DealCards(1);
            }
            return View();
        }
    }
}