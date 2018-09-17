using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Services
{
    public class ProfileService
    {
        private IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public Profile CreateProfile(Profile profile)
        {
            return _profileRepository.Create(profile);
        }

        public Profile GetProfile(int? id)
        {
            return _profileRepository.Get(id);
        }

        public Profile GetProfileByEmail(string email)
        {
            return _profileRepository.GetByEmail(email);
        }

        public Profile EditProfile(Profile profile)
        {
            return _profileRepository.EditProfile(profile);
        }
    }
}
