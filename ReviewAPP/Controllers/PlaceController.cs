using Microsoft.AspNetCore.Mvc;
using ReviewAPP.Models;
using ReviewAPP.Interfaces;
using AutoMapper;
using ReviewAPP.Dto;
using ReviewAPP.Repository;

namespace ReviewAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        public PlaceController(IPlaceRepository placeRepository, IMapper mapper,IReviewRepository reviewRepository) {
            _placeRepository = placeRepository; 
            _mapper = mapper;   
            _reviewRepository = reviewRepository;   
        
        }


        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Place>))]
        public IActionResult getPlaces() 
        {
            var places =_mapper.Map<List<PlaceDto>>(_placeRepository.GetPlaces());

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);      
            
            return Ok(places);
        }

        [HttpGet("{placeID}")]
        [ProducesResponseType(200, Type=typeof(Place))]
        [ProducesResponseType(400)]
        public IActionResult getPlace(int placeID) 
        {
            if(!_placeRepository.PlaceExists(placeID))
                return NotFound();  

            var place = _mapper.Map<Place>(_placeRepository.GetPlace(placeID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  
            
            
            return Ok(place);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlace([FromQuery] int catID, [FromBody] PlaceDto newPlace) 
        {
            if (newPlace == null)
                return BadRequest(ModelState);

            var places = _placeRepository.GetPlaces().
                        Where(p => p.Name.Trim().ToUpper() == newPlace.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (places != null)
            {
                ModelState.AddModelError("", "Place already exists");
                return StatusCode(422, ModelState);
            }
            var placeMap = _mapper.Map<Place>(newPlace);

            if (!_placeRepository.CreatePlace(catID, placeMap)) 
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Created");
        
        }
        [HttpPut("{placeID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePlace(int placeID, [FromBody] PlaceDto updatePlace)
        {
            if (updatePlace == null)
                return BadRequest(ModelState);

            if (placeID != updatePlace.ID)
                return BadRequest(ModelState);

            if (!_placeRepository.PlaceExists(placeID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var placeMap = _mapper.Map<Place>(updatePlace);

            if (!_placeRepository.UpdatePlace(placeMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{placeID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlace(int placeID)
        {
            if (!_placeRepository.PlaceExists(placeID))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfPlace(placeID);
            var placeToDelete = _placeRepository.GetPlace(placeID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_placeRepository.DeletePlace(placeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

    }
}
