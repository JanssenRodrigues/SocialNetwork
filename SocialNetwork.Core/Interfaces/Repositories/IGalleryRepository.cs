using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces.Repositories
{
    public interface IGalleryRepository
    {
         Gallery Create(Gallery gallery);
         Gallery GetGallery(int id);
         Gallery AddPhoto(Photo photo);
    }
}
