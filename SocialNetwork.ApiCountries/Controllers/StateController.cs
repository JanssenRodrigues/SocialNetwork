using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SocialNetwork.ApiCountries.Controllers
{
    public class StateController : ApiController
    {
        //GET api/State/all
        [Route("api/state/all")]
        [ResponseType(typeof(IEnumerable<State>))]
        public IHttpActionResult GetAll()
        {
            StateStoredProcedureRepository storedState = new StateStoredProcedureRepository();
            var states = storedState.GetAll();

            return Ok(states);
        }

        //POST: api/State
        [HttpPost]
        [Route("api/state")]
        [ResponseType(typeof(State))]
        public IHttpActionResult CreateState(State state)
        {
            StateStoredProcedureRepository storedState = new StateStoredProcedureRepository();
            storedState.Create(state);

            return Ok(state);
        }
    }
}
