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
    public class CountriesController : ApiController
    {

        //GET api/Country/all
        [Route("api/country/all")]
        [ResponseType(typeof(IEnumerable<Country>))]
        public IHttpActionResult GetAll()
        {
            CountryStoredProcedureRepository storedCountry = new CountryStoredProcedureRepository();
            var countries = storedCountry.GetAll();

            return Ok(countries);
        }

        //POST: api/Country
        [HttpPost]
        [Route("api/country")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult CreateCountry(Country country)
        {
            CountryStoredProcedureRepository storedCountry = new CountryStoredProcedureRepository();
            storedCountry.Create(country);

            return Ok(country);
        }

        //GET: api/Country/:id
        [HttpGet]
        [Route("api/country/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(int id)
        {

            CountryStoredProcedureRepository storedCountry = new CountryStoredProcedureRepository();
            Country country = storedCountry.Get(id);

            return Ok(country);
        }

        //POST: api/Country/edit/:country
        [HttpPost]
        [Route("api/country/edit")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult EditCounry(Country country)
        {
            CountryStoredProcedureRepository storedCountry = new CountryStoredProcedureRepository();
            storedCountry.Edit(country);

            return Ok(country);
        }


        //POST: api/Country/delete/:id
        [HttpDelete]
        [Route("api/country/delete/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCounry(int id)
        {
            CountryStoredProcedureRepository storedCountry = new CountryStoredProcedureRepository();
            Country country = storedCountry.Get(id);
            storedCountry.Delete(country);

            return Ok(country);
        }
    }
}
