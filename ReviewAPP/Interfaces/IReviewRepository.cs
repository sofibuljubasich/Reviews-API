using Microsoft.AspNetCore.Mvc;
using ReviewAPP.Models;
namespace ReviewAPP.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int reviewID);

        ICollection<Review> GetReviewsOfPlace(int placeID);

        bool ReviewExists(int reviewID);    

        bool CreateReview(int reviewerID, int placeID, Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);

        bool Save();
    }
}
