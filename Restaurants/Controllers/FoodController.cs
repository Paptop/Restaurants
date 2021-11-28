using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain;
using Restaurants.Persistence;

namespace Restaurants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodController : Controller
    {
        private IRestaurantRepository _restaurantRepo;

        public FoodController(IRestaurantRepository repo)
        {
            _restaurantRepo = repo;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Restaurant),200)]
        public IActionResult Details(int id)
        {
            int vId = id;
            Restaurant res = null;
            if (_restaurantRepo.TryGetById(vId, ref res))
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add(Restaurant rs)
        {
            Restaurant result = null;

            if((result = _restaurantRepo.Create(rs)) == null)
            {
                return BadRequest();
            }

            return new CreatedResult("",result);
        }

        [HttpPut]
        public IActionResult Update(Restaurant rs)
        {
            Restaurant result = null;
            if((result = _restaurantRepo.Update(rs)) == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            int vId = id;
            Restaurant res = null;
            if (_restaurantRepo.TryGetById(vId, ref res))
            {
                _restaurantRepo.Delete(res);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_restaurantRepo.GetAll());
        }
    }
}
