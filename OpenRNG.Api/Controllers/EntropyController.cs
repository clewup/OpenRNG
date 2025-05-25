using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OpenRNG.Api.Models;
using OpenRNG.Core.Services.Interfaces;

namespace OpenRNG.Api.Controllers;

[ApiController]
[Route("entropy")]
public class EntropyController(IEntropyService entropyService) : ControllerBase
{
    [HttpPost]
    public IActionResult CalculateEntropy([FromBody] EntropyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Input))
        {
            return BadRequest(new
            {
                error = "Validation error",
                message = "Input is required",
                parameters = new { request.Input }
            });
        }

        return Ok(new EntropyResponse()
        {
            ShannonEntropy = entropyService.CalculateShannonEntropy(request.Input),
            MinEntropy = entropyService.CalculateMinEntropy(request.Input),
            RenyiEntropy = entropyService.CalculateRenyiEntropy(request.Input, 2),
            Distribution = entropyService.CalculateDistribution(request.Input)
        });
    }
}