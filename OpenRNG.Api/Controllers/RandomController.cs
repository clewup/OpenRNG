using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OpenRNG.Api.Models;
using OpenRNG.Core.Services.Interfaces;

namespace OpenRNG.Api.Controllers;

[ApiController]
[Route("random")]
public class RandomController(IRandomService randomService) : ControllerBase
{
    [HttpGet("integer")]
    public IActionResult GetRandomInteger([FromQuery] int min = 0, [FromQuery] int max = Int32.MaxValue - 1)
    {
        if (min >= max)
        {
            return BadRequest(new ErrorResponse
            {
                Error = "Validation error",
                Message = "The 'min' query parameter must be less than 'max'.",
                Parameters = new { min, max }
            });
        }
        
        var integer = randomService.GetSecureRandomInt(min, max);
        
        return Ok(new RandomResponse()
        {
            Type = "integer",
            Value = integer,
            GeneratedAt = DateTime.UtcNow
        });
    }
    
    [HttpGet("password")]
    public IActionResult GetRandomPassword([FromQuery] int length = 12, [FromQuery] bool includeSymbols = false)
    {
        if (length <= 0)
        {
            return BadRequest(new
            {
                error = "Validation error",
                message = "Password length must be greater than zero",
                parameters = new { length, includeSymbols }
            });
        }
        
        var password = randomService.GetSecureRandomPassword(length, includeSymbols);

        return Ok(new RandomResponse()
        {
            Type = "password",
            Value = password,
            GeneratedAt = DateTime.UtcNow
        });
    }
    
    [HttpGet("lorem")]
    public IActionResult GetRandomLoremIpsum([FromQuery] int words = 10)
    {
        if (words <= 0)
        {
            return BadRequest(new
            {
                error = "Validation error",
                message = "Word length must be greater than zero",
                parameters = new { words }
            });
        }
        
        var text = randomService.GenerateLoremIpsum(words);
        
        return Ok(new RandomResponse()
        {
            Type = "lorem",
            Value = text,
            GeneratedAt = DateTime.UtcNow
        });
    }
    
    [HttpGet("avatar")]
    public IActionResult GetRandomAvatar([FromQuery] string seed, [FromQuery] string style = "initials")
    {
        if (string.IsNullOrWhiteSpace(seed))
        {
            return BadRequest(new
            {
                error = "Validation error",
                message = "Seed query parameter is required",
                parameters = new { seed, style }
            });
        }
        
        var url = randomService.GenerateAvatarUrl(seed, style);
            
        return Ok(new RandomResponse()
        {
            Type = "avatar",
            Value = url,
            GeneratedAt = DateTime.UtcNow
        });
    }
    
    [HttpGet("color")]
    public IActionResult GetRandomColor()
    {
        var color = randomService.GetRandomHexColor();

        return Ok(new RandomResponse()
        {
            Type = "color",
            Value = color,
            GeneratedAt = DateTime.UtcNow
        });
    }
}