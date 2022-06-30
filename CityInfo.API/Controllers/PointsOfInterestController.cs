using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{CityId}/PointsOfInterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestDtos>> GetPointsOfInterest(int CityId)
        {
        
           var city= CitiesDataStore.Current.Cities.FirstOrDefault(c=>c.Id==CityId);
            if(city==null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointofinterestid}")]
        public ActionResult<PointsOfInterestDtos> GetPointsOfInterest(int CityId, int pointofinterestid)
        {

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                return NotFound();

            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(p=>p.Id== pointofinterestid);
            if (pointsOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointsOfInterest);
        }

    }
}
