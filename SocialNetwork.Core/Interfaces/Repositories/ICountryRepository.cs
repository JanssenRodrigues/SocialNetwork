using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Country Get(int? id);
        IEnumerable<Country> GetAll();
        Country Create(Country country);
        Country Edit(Country country);
        Country Delete(Country country);
    }
}
