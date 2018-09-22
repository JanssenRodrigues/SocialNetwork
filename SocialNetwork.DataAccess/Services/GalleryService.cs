using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Services
{
    public class GalleryService
    {
        private IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public Gallery Create(Gallery gallery)
        {
            return _galleryRepository.Create(gallery);
        }

        public Gallery GetGallery(int id)
        {
            return _galleryRepository.GetGallery(id);
        }

        public Gallery AddPhoto(Photo photo)
        {
            return _galleryRepository.AddPhoto(photo);
        }
    }
}
