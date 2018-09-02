using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View("Profile");
        }



        // GET: Profile/Create
        public ActionResult Create()
        {
            return View("CreateProfile");
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProfileViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }

            if(ModelState.IsValid)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56331/");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Baerer", $"{access_token}");

                    var response = await client.PostAsJsonAsync("api/Profiles", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Profile", "Profile");
                    } else
                    {
                        return View("Error");
                    }
                }
            }

            return View();
        }
    }
}