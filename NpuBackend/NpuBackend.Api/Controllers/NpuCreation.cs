using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NpuBackend.Api.DTOs;
using NpuBackend.Domain.Models;
using NpuBackend.Services.Interfaces;

namespace NpuBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NpuCreationController : ControllerBase
    {
        private readonly INpuCreationService _npuCreationService;
        private readonly IBlobStorageService _blobStorageService;

        public NpuCreationController(INpuCreationService npuCreationService, IBlobStorageService blobStorageService)
        {
            _npuCreationService = npuCreationService;
            _blobStorageService = blobStorageService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var creation = await _npuCreationService.GetByIdAsync(id);
            if (creation == null) return NotFound();
            var responseDto = new NpuCreationResponseDto
            {
                Id = creation.Id,
                Title = creation.Title,
                Description = creation.Description,
                UserId = creation.UserId,
                ElementIds = creation.Elements?.Select(e => e.ElementId).ToList() ?? [],
                ImageUrl = _blobStorageService.GetImageUrl(creation.ImageUrl),
                CreatedAt = creation.CreatedAt,
                TotalScore = creation.Scores?.Sum(score => score.Value) ?? 0
            };

            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] List<Guid>? elementIds = null)
        {
            var creations = await _npuCreationService.GetAllAsync();

            if (elementIds != null && elementIds.Count > 0)
            {
                creations = creations.Where(creation => 
                    creation.Elements.Count() > 0 && 
                    creation.Elements.Any(e => elementIds.Contains(e.ElementId))
                ).ToList();
            }

            var responseDtos = creations.Select(creation => new NpuCreationResponseDto
            {
                Id = creation.Id,
                Title = creation.Title,
                Description = creation.Description,
                UserId = creation.UserId,
                ElementIds = creation.Elements?.Select(e => e.ElementId).ToList() ?? [],
                ImageUrl = _blobStorageService.GetImageUrl(creation.ImageUrl),
                CreatedAt = creation.CreatedAt,
                TotalScore = creation.Scores?.Sum(score => score.Value) ?? 0
            }).ToList();

            return Ok(responseDtos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NpuCreationRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var fileName = $"{Guid.NewGuid()}/{request.ImageFile.FileName}";

            string imagePrefix;
            await using (var stream = request.ImageFile.OpenReadStream())
            {
                imagePrefix = await _blobStorageService.UploadImageAsync(fileName, stream);
            }

            var creation = new NpuCreation
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                UserId = userId,
                ImageUrl = imagePrefix,
                CreatedAt = DateTime.UtcNow,
            };

            creation.Elements = request.ElementIds.Select(id => new NpuCreationElement
            {
                NpuCreationId = creation.Id,
                ElementId = id
            }).ToList();

            await _npuCreationService.CreateAsync(creation);
            var responseDto = new NpuCreationResponseDto
            {
                Id = creation.Id,
                Title = creation.Title,
                Description = creation.Description,
                UserId = creation.UserId,
                ElementIds = creation.Elements?.Select(e => e.ElementId).ToList() ?? [],
                ImageUrl = _blobStorageService.GetImageUrl(creation.ImageUrl),
                CreatedAt = creation.CreatedAt,
                TotalScore = creation.Scores?.Sum(score => score.Value) ?? 0
            };

            return CreatedAtAction(nameof(GetById), new { id = creation.Id }, responseDto);
        }

    }
}