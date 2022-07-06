using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{pointofinterestid}", Name = "GetPointsOfInterest")]
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

        [HttpPost]
        public ActionResult<PointsOfInterestDtos> CreatePointOfInterest(int CityId, 
            PointOfInterestForCreationDto pointOfInterest // Complex type always comes FromBody
            )
        {

           
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                return NotFound();

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointsOfInterestDtos()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };
            city.PointsOfInterest.Add(finalPointOfInterest);    

            return CreatedAtRoute(nameof(GetPointsOfInterest), 
                new {    CityId = CityId,pointOfInterestId= finalPointOfInterest.Id  }, finalPointOfInterest); 

        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult<PointsOfInterestDtos> UpdatePointOfInterest(int CityId,
           int pointOfInterestId, PointOfInterestforUpdateDto pointOfInterest
           )
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int CityId,
           int pointOfInterestId, JsonPatchDocument<PointOfInterestforUpdateDto> patchDocument
           )
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestforUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description,
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Ensuring that Patch does not go against the Data annotation defined in the model
            // Like Name is a required field but we can use a replace to remove the name from model
            if (!TryValidateModel(pointOfInterestToPatch))
            { 
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            return NoContent();

        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int CityId,
           int pointOfInterestId
           )
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            return NoContent();
        }




        }
}
