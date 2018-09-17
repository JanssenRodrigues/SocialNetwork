using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Profile Create(Profile profile);
        Profile Get(int? id);
        Profile GetByEmail(string email);
        Profile EditProfile(Profile profile);
    }
}
