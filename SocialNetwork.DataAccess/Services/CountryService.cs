using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Services
{
    public class CountryService
    {
        private ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Country CreateCountry(Country country)
        {
            return _countryRepository.Create(country);
        }

        public Country EditCountry(Country country)
        {
            return _countryRepository.Edit(country);
        }

        public Country DeleteCountry(Country country)
        {
            return _countryRepository.Delete(country);
        }

        public Country GetCountry(int? id)
        {
            return _countryRepository.Get(id);
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _countryRepository.GetAll();
        }


    }
}
