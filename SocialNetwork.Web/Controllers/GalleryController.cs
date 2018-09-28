using AzureStorageService;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
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
    public class GalleryController : Controller
    {

        private HttpClient _client;

        public GalleryController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:56331/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void RegisterClientToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Baerer " + Session["apiToken"].ToString());
        }

        // GET: Gallery
        [HttpGet]
        public ActionResult CreateGallery()
        {
            return View();
        }

        // POST: Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGallery([Bind(Include = "Id,Name")] Gallery gallery)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
                Profile createdProfile = profileStored.GetByEmail(Session["userEmail"].ToString());

                gallery.ProfileId = createdProfile.Id;
                await _client.PostAsJsonAsync<Gallery>("api/gallery", gallery);


                return RedirectPermanent("/Profile/Details/" + createdProfile.Id);
            }

            return View(gallery);
        }

        // GET: Gallery/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            RegisterClientToken();
            Gallery gallery = new Gallery();

            gallery = _client.GetAsync("api/Gallery/" + id).Result.Content.ReadAsAsync<Gallery>().Result;

            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile createdProfile = profileStored.GetByEmail(Session["userEmail"].ToString());
            ViewBag.LoggedUserId = createdProfile.Id;

            return View(gallery);
        }

        // GET: Gallery/Edit/:id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RegisterClientToken();
            Gallery gallery = new Gallery();

            gallery = _client.GetAsync("api/Gallery/" + id).Result.Content.ReadAsAsync<Gallery>().Result;

            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile createdProfile = profileStored.GetByEmail(Session["userEmail"].ToString());
            ViewBag.LoggedUserId = createdProfile.Id;

            return View(gallery);
        }

        // POST: Gallery/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Gallery gallery)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                await _client.PostAsJsonAsync<Gallery>("api/Gallery/Edit/" + gallery.Id, gallery);
            }
            return RedirectPermanent("/Gallery/Details/" + gallery.Id);
        }

        //POST: Gallery/AddPhoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoto(HttpPostedFileBase Photo)
        {
            RegisterClientToken();
            Photo photo = new Photo();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = Photo;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                photo.Url = fileUrl;
                photo.GalleryId = int.Parse(Request.UrlReferrer.Segments[3].ToString());
                //#######################################

                await _client.PostAsJsonAsync("api/Gallery/AddPhoto", photo);

                return RedirectPermanent("/Gallery/Details/" + photo.GalleryId);
                //return View()/
            }
            return View();
        }


        // GET: Gallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = _client.GetAsync("api/gallery/" + id)
                .Result.Content.ReadAsAsync<Gallery>().Result;
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterClientToken();
            Gallery gallery = null;

            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deleteResult = _client.DeleteAsync("api/gallery/delete/" + id).Result;
            if (deleteResult.IsSuccessStatusCode)
            {
                gallery = deleteResult.Content.ReadAsAsync<Gallery>().Result;
            }

            if (gallery == null)
            {
                return HttpNotFound();
            }

            return RedirectPermanent("/Profile/Details/" + gallery.ProfileId);
        }

    }
}