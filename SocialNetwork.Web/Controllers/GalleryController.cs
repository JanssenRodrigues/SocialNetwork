using AzureStorageService;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

            gallery = _client.GetAsync("api/Gallery/" + id).Result.Content.ReadAsAsync<Gallery>().Result;


            return View(gallery);
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


    }
}