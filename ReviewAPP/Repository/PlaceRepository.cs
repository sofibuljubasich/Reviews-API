using Microsoft.EntityFrameworkCore;
using ReviewAPP.Data;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;

namespace ReviewAPP.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext _context;

        public PlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreatePlace(int categoryID, Place place)
        {
            var category = _context.Categories.Where(c => c.Id == categoryID).FirstOrDefault();

            var placeCategory = new PlaceCategory()
            {
                Category = category,
                Place = place,
            };
            _context.Add(placeCategory);
            _context.Add(place);
            return Save();
        }

        public Place GetPlace(int id)
        {
            return _context.Places.Where(p => p.Id == id).FirstOrDefault();

        }

        public Place GetPlace(string name)
        {
            return _context.Places.Where(p => p.Name == name).FirstOrDefault();

        }
        public ICollection<Place> GetPlaces()
        {
            return _context.Places.OrderBy(p => p.Id).ToList();
        }

       

        public bool PlaceExists(int id)
        {
            return _context.Places.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public decimal GetPlaceRating(int placeID)
        {
            var review = _context.Reviews.Where(p => p.Place.Id == placeID);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Stars) / review.Count());
        }

        public bool UpdatePlace(Place place) 
        {
            _context.Update(place);
            return Save();
        }

        public bool DeletePlace(Place place)
        {
            _context.Remove(place);
            return Save();
        }
    }
}
