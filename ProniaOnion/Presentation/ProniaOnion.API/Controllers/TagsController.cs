using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
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

            var getTagDTO = await _service.GetByIdAsync(id);

            if (getTagDTO == null) return NotFound();
            return Ok(getTagDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTagDto tagDto)
        {
            await _service.CreateAsync(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTagDto tagDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, tagDto);

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

