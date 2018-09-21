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
    public class FollowController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FollowService _followService = new FollowService(new FollowStoredProcedureRepository());

        //GET: api/Follow/:id/:id
        [Route("api/Follow/{id:int}/{followingId:int}")]
        [ResponseType(typeof(Follow))]
        public IHttpActionResult GetFollow(int id, int followingId)
        {

            Follow follow = new Follow();
            FollowStoredProcedureRepository followStored = new FollowStoredProcedureRepository();
            follow = followStored.CheckFollow(id, followingId);

            return Ok(follow);
        }

        //POST: api/Follow/id
        [Route("api/Follow")]
        [ResponseType(typeof(void))]
        public IHttpActionResult FollowProfile(Follow follow)
        {
            FollowStoredProcedureRepository followStored = new FollowStoredProcedureRepository();
            followStored.FollowProfile(follow);

            return Ok(follow);
        }


        //POST: api/Follow/id
        [Route("api/Follow/{id:int}/{unfollowId:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteFollow(int id, int unfollowId)
        {

            FollowStoredProcedureRepository followStored = new FollowStoredProcedureRepository();
            Follow follow = followStored.DeleteFollow(id, unfollowId);

            return Ok(follow);
        }


    }
}
