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
                IRepository<Card> repository = new CardRepository(db);
                var card = new Card { Suit = Suit.Hearts, Value = Value.King, Key = 10, DateOfCreation = DateTime.Now };
                await repository.Create(card);
            }
            return View();
        }
    }
}