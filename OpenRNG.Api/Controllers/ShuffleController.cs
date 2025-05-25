using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OpenRNG.Api.Models;
using OpenRNG.Api.Services;
using OpenRNG.Api.Services.Interfaces;

namespace OpenRNG.Api.Controllers;

[ApiController]
[Route("shuffle")]
public class ShuffleController(IShuffleService shuffleService) : ControllerBase
{
    [HttpPost]
    public IActionResult ShuffleList([FromBody] List<object> items)
    {
        if (items == null || items.Count == 0)
        {
            return BadRequest(new
            {
                error = "Validation error",
                message = "Items are required",
                parameters = new { items }
            });
        }

        shuffleService.Shuffle(items);

        return Ok(new ShuffleResponse
        {
            Shuffled = items
        });
    }
}