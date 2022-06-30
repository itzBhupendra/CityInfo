using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }


        [HttpGet("{id}")]
        public ActionResult<CityInfo.API.Models.CityDto> GetCity(int id)
        {
            var City = CitiesDataStore.Current.Cities.FirstOrDefault(a => a.Id == id);
            if (City == null)
                return NotFound();
            return  Ok(City);
        }

    }
}
