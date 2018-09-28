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
         Photo AddPhoto(Photo photo);
         Gallery GetPhotos(Gallery gallery);
         Gallery Delete(Gallery gallery);
         Gallery Edit(Gallery gallery);
    }
}
