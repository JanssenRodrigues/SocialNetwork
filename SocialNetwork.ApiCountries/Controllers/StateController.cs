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
            CountryStoredProcedureRepository countryState = new CountryStoredProcedureRepository();
            var states = storedState.GetAll();

            foreach (var state in states)
            {
                var country = countryState.Get(state.CountryId);
                state.CountryName = country.Name;
            }

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

        // GET: api/state/:id
        [HttpGet]
        public IHttpActionResult GetState(int id)
        {
            StateStoredProcedureRepository storedState = new StateStoredProcedureRepository();
            State state = storedState.Get(id);

            return Ok(state);
        }

        //POST: api/State/edit
        [HttpPost]
        [Route("api/state/edit")]
        [ResponseType(typeof(State))]
        public IHttpActionResult EditState(State state)
        {
            StateStoredProcedureRepository storedState = new StateStoredProcedureRepository();
            storedState.Edit(state);

            return Ok(state);
        }

        //POST: api/State/delete/:id
        [HttpDelete]
        [Route("api/state/delete/{id}")]
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(int id)
        {
            StateStoredProcedureRepository storedState = new StateStoredProcedureRepository();
            State state = storedState.Get(id);
            storedState.Delete(state);

            return Ok(state);
        }

    }
}
