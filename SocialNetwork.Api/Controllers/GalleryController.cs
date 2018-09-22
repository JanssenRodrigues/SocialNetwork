using SocialNetwork.Api.Models;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SocialNetwork.Api.Controllers
{
    public class GalleryController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private GalleryService _galleryService = new GalleryService(new GalleryStoredProcedureRepository());

        // GET: api/Gallery/5
        [Route("api/Gallery/{id:int}")]
        public IHttpActionResult GetGallery(int id)
        {
            Gallery gallery = new Gallery();
            GalleryStoredProcedureRepository galleryStored = new GalleryStoredProcedureRepository();
            gallery = galleryStored.GetGallery(id);

            gallery = galleryStored.GetPhotos(gallery);


            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }


        // GET: api/GalleryByProfileId/5
        [Route("api/GalleryByProfileId/{profileId:int}")]
        public IHttpActionResult GetGalleriesByProfileId(int profileId)
        {
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile profile = profileStored.Get(profileId);

            GalleryStoredProcedureRepository galleryStored = new GalleryStoredProcedureRepository();
           profile = galleryStored.GetGalleriesByProfileId(profile);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // POST: api/Gallery
        [HttpPost]
        public IHttpActionResult Post(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Galleries.Add(gallery);

            _galleryService.Create(gallery);

            return CreatedAtRoute("DefaultApi", new { id = gallery.Id }, gallery);
        }

        // POST: api/Gallery/AddPhoto
        [HttpPost]
        [Route("api/Gallery/AddPhoto")]
        public IHttpActionResult AddPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GalleryStoredProcedureRepository storedGallery = new GalleryStoredProcedureRepository();
            storedGallery.AddPhoto(photo);

            return CreatedAtRoute("DefaultApi", new { id = photo.GalleryId}, photo);
        }
            // PUT: api/Gallery/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Gallery/5
        public void Delete(int id)
        {
        }
    }
}
