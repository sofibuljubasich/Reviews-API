using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;
using ReviewAPP.Dto;
using ReviewAPP.Repository;

namespace ReviewAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewController(IReviewRepository reviewRepository, 
                    IMapper mapper,IPlaceRepository placeRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _placeRepository = placeRepository; 
            _reviewerRepository = reviewerRepository;   
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }
        [HttpGet("reviewID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewID)
        {
            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewID));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("place/{placeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfPlace(int placeID)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfPlace(placeID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerID, [FromQuery] int placeID,[FromBody] ReviewDto newReview)
        {
            if (newReview == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews().
                        Where(r => r.Title.Trim().ToUpper() == newReview.Title.TrimEnd().ToUpper()).FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }
            var reviewMap = _mapper.Map<Review>(newReview);

            reviewMap.Place = _placeRepository.GetPlace(placeID);   
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerID);

            if (!_reviewRepository.CreateReview(reviewerID,placeID,reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Created");

        }
        [HttpPut("{reviewID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewID, [FromBody] ReviewDto updateReview)
        {
            if (updateReview == null)
                return BadRequest(ModelState);

            if (reviewID != updateReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updateReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
    }
