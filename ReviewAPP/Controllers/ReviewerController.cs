using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPP.Dto;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;
using ReviewAPP.Repository;

namespace ReviewAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepository reviewerRepository,IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;   
            _mapper = mapper;   
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers() 
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);      
            return Ok(reviewers);

        }

        [HttpGet("{reviewerID}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerID) 
        {
            if (!_reviewerRepository.ReviewerExists(reviewerID))   
                return NotFound();  

            var reviewer = _mapper.Map<ReviewDto>(_reviewerRepository.GetReviewer(reviewerID));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);    
        }

        [HttpGet("{reviewerID}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerID) 
        { 
            if(!_reviewerRepository.ReviewerExists(reviewerID))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerID));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody]ReviewerDto newReviewer)
        {
            if (newReviewer == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers().
                    Where(r=>r.Username.Trim().ToUpper() == newReviewer.Username.ToUpper()).FirstOrDefault();

            if (reviewer != null) 
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422,ModelState);  
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(newReviewer);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }


        [HttpPut("{reviewerID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerID, [FromBody]ReviewerDto newReviewer)
        {
            if (newReviewer == null)
                return BadRequest(ModelState);

            if (reviewerID != newReviewer.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewerExists(reviewerID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(newReviewer);

            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{reviewerID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerID)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerID))
            {
                return NotFound();
            }

            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
