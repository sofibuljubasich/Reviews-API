using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;
using ReviewAPP.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ReviewAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
         }

        [HttpGet("{categoryID}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryID) 
        {
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryID));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(category);    
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Category>))]
        public IActionResult GetCategories() 
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("place/{categoryID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Place>))]
        [ProducesResponseType(400)]
        public IActionResult GetPlaceByCategory(int categoryID) 
        { 
            var places = _mapper.Map<List<PlaceDto>>(
                            _categoryRepository.GetPlaceByCategory(categoryID));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(places);
        
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto newCat)
        {
            if (newCat == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories().
                        Where(c => c.Name.Trim().ToUpper() == newCat.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(newCat);

            if (!_categoryRepository.CreateCategory(categoryMap)) 
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);  
            }
            return Ok("Succesfully created");

        }

        [HttpPut("{categoryID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryID, [FromBody] CategoryDto newCat) 
        {
            if (newCat == null)
                return BadRequest(ModelState);

            if (categoryID != newCat.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(newCat);

            if (!_categoryRepository.UpdateCategory(categoryMap)) 
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        
        }

        [HttpDelete("{categoryID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryID)
        {
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();

            var catToDelete = _categoryRepository.GetCategory(categoryID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(catToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong");

            }
            return NoContent();
        } 

    }
}
