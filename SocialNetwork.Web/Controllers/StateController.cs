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
    public class StateController : Controller
    {
        private HttpClient _client;

        public StateController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:62651/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void RegisterClientToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Baerer " + Session["apiToken"].ToString());
        }

        // GET: State
        public ActionResult Index()
        {
            RegisterClientToken();
            return View(_client.GetAsync("api/State/all")
                .Result.Content
                .ReadAsAsync<IEnumerable<State>>().Result);
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            State state = _client.GetAsync("api/State/" + id).Result.Content.ReadAsAsync<State>().Result;

            return View(state);
        }

        // GET: State/Create
        public ActionResult Create()
        {
            List<Country> countries = (List<Country>)_client.GetAsync("api/country/all").Result.Content.ReadAsAsync<IEnumerable<Country>>().Result;
            ViewBag.CountryList = countries;
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PhotoUrl,CountryId")] State state, HttpPostedFileBase PhotoUrl)
        {
            RegisterClientToken();
            if (ModelState.IsValid)
            {
                //##### Upload da Foto para o Blob #####
                HttpPostedFileBase file = PhotoUrl;
                var blobService = new BlobService();
                string fileUrl = await blobService.UploadImage("socialnetwork", Guid.NewGuid().ToString() + file.FileName, file.InputStream, file.ContentType);
                state.PhotoUrl = fileUrl;
                //#######################################
                var response = await _client.PostAsJsonAsync<State>("api/state", state);


                return RedirectPermanent("/State/Index/");
            }

            return View(state);
        }

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: State/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
