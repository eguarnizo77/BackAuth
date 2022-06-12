

using BackAuth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackAuth.Data.Interface
{
    public interface IImageProfileService
    {
        Task<IList<ImageProfile>> GetAllImages();
        Task<ImageProfile> GetImagesById(int id);
    }
}
