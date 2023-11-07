using AutoMapper;
using ReviewAPP.Data;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;

namespace ReviewAPP.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public bool CreateReview(int reviewerID, int placeID, Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewID)
        {
            return _context.Reviews.Where(r => r.Id == reviewID).FirstOrDefault();        
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfPlace(int placeID)
        {
            return _context.Reviews.Where(r=>r.Place.Id==placeID).ToList(); 
        }

        public bool ReviewExists(int reviewID)
        {
            return _context.Reviews.Any(r=>r.Id==reviewID);     
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
