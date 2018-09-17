using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces.Repositories
{
    public interface IFollowRepository
    {
        Follow FollowProfile(Follow follow);
        Follow CheckFollow(int UserId, int FollowingId);
    }
}
