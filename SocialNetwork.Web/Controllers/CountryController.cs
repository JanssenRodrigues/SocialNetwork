using AzureStorageService;
using SocialNetwork.Core.Models;
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
    public class CountryController : Controller
    {
        private HttpClient _client;

        public CountryController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:62651/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void RegisterClientToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Baerer " + Session["apiToken"].ToString());
        }

        // GET: Country
        public ActionResult Index()
        {
            RegisterClientToken();
            return View(_client.GetAsync("api/country/all")
                .Result.Content
                .ReadAsAsync<IEnumerable<Country>>().Result);
        }

        // GET: Country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Country country = _client.GetAsync("api/Country/" + id).Result.Content.ReadAsAsync<Country>().Result;

            return View(country);
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PhotoUrl")] Country country, HttpPostedFileBase PhotoUrl)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = PhotoUrl;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                country.PhotoUrl = fileUrl;
                //#######################################
                await _client.PostAsJsonAsync<Country>("api/country", country);


                return RedirectPermanent("/Country/Index/");
            }

            return View(country);
        }

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {

            Country country = _client.GetAsync("api/Country/" + id).Result.Content.ReadAsAsync<Country>().Result;

            return View(country);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PhotoUrl")] Country country, HttpPostedFileBase PhotoUrl)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = PhotoUrl;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                country.PhotoUrl = fileUrl;
                //#######################################
                await _client.PostAsJsonAsync<Country>("api/country/edit", country);


                return RedirectPermanent("/Country/Index/");
            }

            return View(country);
        }

        // GET: Country/Delete/5
        public ActionResult Delete(int id)
        {
            Country country = _client.GetAsync("api/Country/" + id).Result.Content.ReadAsAsync<Country>().Result;

            return View(country);
        }

        // POST: Country/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,Name,PhotoUrl")] Country country)
        {
            _client.DeleteAsync("api/country/delete/" + country.Id);

            return RedirectToAction("Index");
        }
    }
}
