using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
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
        //POST: api/Follow/id
        [Route("api/Follow")]
        [ResponseType(typeof(void))]
        public IHttpActionResult FollowProfile(Follow follow)
        {
            FollowStoredProcedureRepository followSored = new FollowStoredProcedureRepository();
            followSored.FollowProfile(follow);

            return Ok(follow);
        }


        //GET: api/Follow/CheckFollow/userId/followingId
        [Route("api/Follow/CheckFollow/{userId:int}/{followingId:int}")]
        public IHttpActionResult CheckFollow(int UserId, int FollowingId)
        {
            FollowStoredProcedureRepository followSored = new FollowStoredProcedureRepository();
            Follow follow = new Follow();
            followSored.CheckFollow(UserId, FollowingId);

            return Ok(follow);
        }
    }
}
