using BackAuth.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BackAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ImageProfileController : Controller
    {

        private IImageProfileService _imageProfileService;

        public ImageProfileController(IImageProfileService imageProfileService)
        {
            _imageProfileService = imageProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _imageProfileService.GetAllImages());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await (_imageProfileService.GetImagesById(id)));
        }

    }
}
