using ReviewAPP.Models;

namespace ReviewAPP.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();

        ICollection<Review> GetReviewsByReviewer(int reviewerID);

        Reviewer GetReviewer(int reviewerID);

        bool ReviewerExists(int reviewerID);

        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);

        bool Save();
    }
}
