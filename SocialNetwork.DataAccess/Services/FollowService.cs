using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Services
{
    public class FollowService
    {
        private IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public Follow FollowProfile(Follow follow)
        {
            return _followRepository.FollowProfile(follow);
        }

        public Follow CheckFollow(int UserId, int FollowingId)
        {
            return _followRepository.CheckFollow(UserId, FollowingId);
        }
    }
}
