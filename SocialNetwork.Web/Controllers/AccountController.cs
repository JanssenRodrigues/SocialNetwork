using Newtonsoft.Json.Linq;
using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56331/");

                    var response = await client.PostAsJsonAsync("api/Account/Register", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", model.Username },
                    { "password", model.Password }
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56331");

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await client.PostAsync("/Token", requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            var tokenData = JObject.Parse(responseContent);

                            Session.Add("access_token", tokenData["access_data"]);

                            return RedirectToAction("Index", "Home");

                        }

                        return View("Error");
                    }
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return View("Logout");
        }

        public ActionResult ForgotPass()
        {
            return View("ForgotPass");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPass(RegisterViewModel model)
        {
            await SendGridHelper.sendEmail(model.Email);

            return View("ForgotSucess");
        }

        public ActionResult RecoveryPass()
        {
            return View("RecoveryPass");
        }



        // POST api/Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoveryPass(RecoveryPassViewModel model)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56331/");

                    var response = await client.PostAsJsonAsync("api/Account/RecoveryPassword", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("RecoverySuccess");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View();
        }

    }
}


