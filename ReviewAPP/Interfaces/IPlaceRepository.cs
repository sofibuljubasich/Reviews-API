using ReviewAPP.Data;
using ReviewAPP.Models;
namespace ReviewAPP.Interfaces
{
    public interface IPlaceRepository
    {
        ICollection<Place> GetPlaces();
        Place GetPlace(int id);
        Place GetPlace(string name);
        decimal GetPlaceRating(int placeID);
        bool PlaceExists(int id);
        bool CreatePlace(int categoryID, Place place);
        bool UpdatePlace(Place place);
        bool DeletePlace(Place place);  
        bool Save();
    }
}
