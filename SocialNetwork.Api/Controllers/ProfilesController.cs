using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SocialNetwork.Api.Models;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.Services;

namespace SocialNetwork.Api.Controllers
{
    public class ProfilesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProfileService _profileService = new ProfileService(new ProfileStoredProcedureRepository());

        // GET: api/Profiles
        public IEnumerable<Profile> GetProfiles()
        {
            return db.Profiles;
        }

        // GET: api/Profiles/5
        [Route("api/Profiles/{id:int}")]
        [ResponseType(typeof(Profile))]
        public IHttpActionResult GetProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // GET: api/Profiles/:email
        [Route("{email}")]
        [ResponseType(typeof(Profile))]
        public IHttpActionResult GetByEmail(string email)
        {
            email = email.DecodeBase64();
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile profile = profileStored.GetByEmail(email);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // PUT: api/Profiles/5
        [Route("api/Profiles/{id:int}")]
        public IHttpActionResult PutProfile(int id, Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Profiles.Add(profile);

            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile createdProfile = profileStored.EditProfile(profile);

            _profileService.EditProfile(profile);

            return Ok(profile);

        }

        // POST: api/Profiles
        [ResponseType(typeof(Profile))]
        public IHttpActionResult PostProfile(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Profiles.Add(profile);

            _profileService.CreateProfile(profile);

            return CreatedAtRoute("DefaultApi", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        [Route("api/Profiles/{id:int}")]
        [ResponseType(typeof(Profile))]
        public IHttpActionResult DeleteProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return NotFound();
            }

            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            Profile createdProfile = profileStored.DeleteProfile(profile);

            return Ok(profile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileExists(int id)
        {
            return db.Profiles.Count(e => e.Id == id) > 0;
        }
    }
}