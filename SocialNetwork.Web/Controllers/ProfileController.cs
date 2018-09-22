using AzureStorageService;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class ProfileController : Controller
    {
        private HttpClient _client;

        public ProfileController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:56331/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void RegisterClientToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Baerer " + Session["apiToken"].ToString());
        }

        // GET: Profile
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            RegisterClientToken();
            return View(_client.GetAsync("api/profiles")
                .Result.Content
                .ReadAsAsync<IEnumerable<Profile>>().Result);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            RegisterClientToken();
            Profile profile, loggedProfile;
            Follow follow;
            //Gallery gallery;

            if(id == null)
            {
                if(Session["userEmail"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                profile = _client.GetAsync("api/profiles" + Session["userEmail"].ToString().EncodeBase64())
                    .Result.Content.ReadAsAsync<Profile>().Result;
            } else
            {
                profile = _client.GetAsync("api/profiles/" + id)
                    .Result.Content.ReadAsAsync<Profile>().Result;

                loggedProfile = _client.GetAsync("api/profiles/" + Session["userEmail"].ToString().EncodeBase64())
                        .Result.Content.ReadAsAsync<Profile>().Result;

                //profile.Galleries.Add(_client.GetAsync("api/GalleryByProfileId/" + profile.Id)
                //        .Result.Content.ReadAsAsync<Gallery>().Result);

                follow = _client.GetAsync("api/Follow/" + loggedProfile.Id + "/" + id).Result.Content.ReadAsAsync<Follow>().Result;

                if(follow.Id != 0)
                {
                    ViewBag.Follow = true;
                }
                ViewBag.ProfileId = loggedProfile.Id;

            }

            if(profile == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfileEmail = Session["userEmail"];
            //ViewBag.Following = follow;
            return View(profile);
        }


        // GET: Profile/Create
        public ActionResult Create()
        {
            RegisterClientToken();
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email,Birthday,AccountId,PhotoUrl")] Profile profile, HttpPostedFileBase PhotoUrl)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = PhotoUrl;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                profile.PhotoUrl = fileUrl;
                //#######################################
                await _client.PostAsJsonAsync<Profile>("api/profiles", profile);

                ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
                Profile createdProfile = profileStored.GetByEmail(profile.Email);

                return RedirectPermanent("/Profile/Details/" + createdProfile.Id);
            }

            return View(profile);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            RegisterClientToken();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = _client.GetAsync("api/profiles/" + id)
                .Result.Content.ReadAsAsync<Profile>().Result;
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,BirthDay,PhotoUrl")] Profile profile, HttpPostedFileBase PhotoUrl)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = PhotoUrl;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                profile.PhotoUrl = fileUrl;
                //#######################################
                await _client.PutAsJsonAsync<Profile>("api/profiles/" + profile.Id, profile);

                //ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
                //Profile createdProfile = profileStored.EditProfile(profile);
            }
            return RedirectPermanent("/Profile/Details/" + profile.Id);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = _client.GetAsync("api/profiles/" + id)
                .Result.Content.ReadAsAsync<Profile>().Result;
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterClientToken();
            Profile profile = null;

            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deleteResult = _client.DeleteAsync("api/profiles/" + id).Result;
            if (deleteResult.IsSuccessStatusCode) {
                profile = deleteResult.Content.ReadAsAsync<Profile>().Result;
            }

            if (profile == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
