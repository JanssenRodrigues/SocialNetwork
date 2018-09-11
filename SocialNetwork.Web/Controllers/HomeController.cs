using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var data = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", model.Username },
                { "password", model.Password }
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56331");

                var response = await client.GetAsync("api/Account/UserInfo");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}