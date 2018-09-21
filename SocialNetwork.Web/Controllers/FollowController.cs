using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.Services;

namespace SocialNetwork.Web.Controllers
{
    public class FollowController : Controller
    {
        private HttpClient _client;

        public FollowController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:56331/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void RegisterClientToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Baerer " + Session["apiToken"].ToString());
        }

        //POST: /Follow/FollowProfile/id
        public async Task<Follow> FollowProfile(int id)
        {
            Follow follow = new Follow();
            Profile profile;
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            profile = profileStored.GetByEmail(Session["userEmail"].ToString());
            follow.UserId = profile.Id;
            follow.FollowingId = id;

            await _client.PostAsJsonAsync<Follow>("api/follow", follow);
            return follow;
        }

        //GET: /Follow/CheckFollow/followingId
        [System.Web.Http.Route("Follow/CheckFollow/{id}")]
        public Follow CheckFollow(int id)
        {
            Follow follow = new Follow();
            Profile profile;
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            profile = profileStored.GetByEmail(Session["userEmail"].ToString());

            string url = "api/Follow/" + profile.Id + "/" + id;
            follow = _client.GetAsync(url).Result.Content.ReadAsAsync<Follow>().Result;
            
            return follow;
        }

        // DELETE: Follow/DeleteFollow/5
        [System.Web.Http.Route("Follow/DeleteFollow/{id:int}")]
        public Follow DeleteFollow(int id)
        {
            Profile profile;
            Follow follow = new Follow();
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            profile = profileStored.GetByEmail(Session["userEmail"].ToString());

            string url = "api/Follow/" + profile.Id + "/" + id;
            var deleteResult = _client.DeleteAsync(url).Result;

            if (!deleteResult.IsSuccessStatusCode)
            {
                return follow;
            }
            follow = deleteResult.Content.ReadAsAsync<Follow>().Result;

            return follow;
        }
    }
}
