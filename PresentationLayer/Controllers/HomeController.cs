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

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            //using (GameContext db = new GameContext())
            //{
            //    var service = new GameService(new GameRepository(db), new CardRepository(db), new RoundRepository(db));
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    await service.DealCardToPlayer(db.Users.FirstOrDefault(x => x.Nickname == "ass228"), 1);
            //    //await service.DealCardToDealer(1);
            //    //await service.DealCardToBots(1);
            //}
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            using (GameContext db = new GameContext())
            {
                var user = new User { UserRole = UserRole.PeoplePlayer, Nickname = "ass228" };
                var service = new GameService(new GameRepository(db), new CardRepository(db), new RoundRepository(db), new MoveRepository(db), new MoveCardsRepository(db), new UserGamesRepository(db), new UserRepository(db));
                await service.CreateNewGame(user, 3, 5);
            }
            return View();
        }
    }
}