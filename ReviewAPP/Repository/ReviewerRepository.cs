using Microsoft.EntityFrameworkCore;
using ReviewAPP.Data;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;

namespace ReviewAPP.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly AppDbContext _context; 
        public ReviewerRepository(AppDbContext context) 
        {
            _context = context; 
        } 
        public Reviewer GetReviewer(int reviewerID)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerID).Include(e => e.Reviews).First();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerID)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerID).ToList();
        }

        public bool ReviewerExists(int reviewerID)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerID);
        }

        public bool CreateReviewer(Reviewer reviewer) 
        {
             _context.Add(reviewer);
            return Save();
        }
        public bool Save() 
        {

            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }
    }
}
