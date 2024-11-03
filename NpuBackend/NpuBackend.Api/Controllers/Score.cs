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
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddScore([FromBody] ScoreRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            if (request.Value < 0) return BadRequest("Score value must be non-negative.");

            var score = new Score
            {
                UserId = userId,
                NpuCreationId = request.CreationId,
                Value = request.Value
            };

            await _scoreService.AddAsync(score);

            return Ok(score);
        }

        [HttpGet("{creationId:guid}")]
        public async Task<IActionResult> GetScores(Guid creationId)
        {
            var scores = await _scoreService.GetAllScoresForCreationAsync(creationId);
            return Ok(scores);
        }
    }
}