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
            //profile = _client.GetAsync("api/profiles/" + Session["userEmail"].ToString().EncodeBase64()).Result.Content.ReadAsAsync<Profile>().Result;
            follow.UserId = profile.Id;
            follow.FollowingId = id;
            var response = await _client.GetAsync("api/Follow/CheckFollow/" + follow.UserId + "/" + follow.FollowingId);
            await _client.PostAsJsonAsync<Follow>("api/follow", follow);
            return follow;
        }

        //POST: /Follow/CheckFollow/id
        public async Task<Follow> CheckFollow(int id)
        {
            Follow follow = new Follow();
            Profile profile;
            ProfileStoredProcedureRepository profileStored = new ProfileStoredProcedureRepository();
            profile = profileStored.GetByEmail(Session["userEmail"].ToString());
            follow.UserId = profile.Id;
            follow.FollowingId = id;

            await _client.GetAsync("api/Follow/CheckFollow/" + id);
            return follow;
        }
    }
}
