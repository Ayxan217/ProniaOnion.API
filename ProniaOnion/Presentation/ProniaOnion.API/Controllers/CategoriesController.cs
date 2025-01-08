using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.Validators;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _service;
        private readonly IValidator<CreateCategoryDto> _validator;

        public CategoriesController(ICategoryService service, IValidator<CreateCategoryDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var getCategoryDTO = await _service.GetByIdAsync(id);

            if (getCategoryDTO == null) return NotFound();
            return Ok(getCategoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
           var result = await _validator.ValidateAsync(categoryDto);

           if(!result.IsValid)
           {
                foreach(ValidationFailure error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }
                return BadRequest(ModelState);
           }
            await _service.CreateAsync(categoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto categoryDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, categoryDto);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
