using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto(){ Id  =1, Name="New Delhi", Description="Good City" 
                
                 ,PointsOfInterest = new List<PointsOfInterestDtos> { 
                     new PointsOfInterestDtos(){ 
                         Id=1, Name="Delhi hat", Description="Shopping" 
                     } 
                 }
                 },
                new CityDto(){ Id  =2, Name="Gorakhpur", Description="Nice City"
                ,PointsOfInterest = new List<PointsOfInterestDtos> { new PointsOfInterestDtos(){ Id=1, Name="Gorakhnath Temple", Description="Very Beautiful temple" } }
                },
                new CityDto(){ Id  =3, Name="Jaipur", Description="Pink City"
                ,PointsOfInterest = new List<PointsOfInterestDtos> { new PointsOfInterestDtos(){ Id=1, Name="City Palace", Description="Awesome palace" } }

                }

            }; 
        }
    }
}
