using BackAuth.Data.Entity;
using BackAuth.Data.Interface;
using BackAuth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackAuth.Data.Service
{    
    public class ImageProfileService : IImageProfileService
    {
        private readonly IImageProfileEntity _imageProfileEntity;
        public ImageProfileService(IImageProfileEntity imageProfileEntity)
        {
            _imageProfileEntity = imageProfileEntity;
        }

        public Task<IList<ImageProfile>> GetAllImages()
        {
            return _imageProfileEntity.GetAll();
        }
        public Task<ImageProfile> GetImagesById(int id)
        {
            return _imageProfileEntity.Get(id);
        }

    }
}
