using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NpuBackend.Domain.Models;
using NpuBackend.Services.Interfaces;

namespace NpuBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementController : ControllerBase
    {
        private readonly IElementService _elementService;

        public ElementController(IElementService elementService)
        {
            _elementService = elementService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var element = await _elementService.GetByIdAsync(id);
            if (element == null) return NotFound();
            return Ok(element);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var elements = await _elementService.GetAllAsync();
            return Ok(elements);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Element element)
        {
            var existingElement = await _elementService.GetByIdAsync(element.ElementId);
            
            if (existingElement != null)
            {
                return Conflict(existingElement);
            }
            
            await _elementService.AddAsync(element);
            return CreatedAtAction(nameof(GetById), new { id = element.ElementId }, element);
        }
    }
}