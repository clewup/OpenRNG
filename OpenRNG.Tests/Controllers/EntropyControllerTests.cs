using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenRNG.Api.Controllers;
using OpenRNG.Api.Models;
using OpenRNG.Api.Services.Interfaces;

namespace OpenRNG.Tests.Controllers;

public class EntropyControllerTests
{
    private readonly Mock<IEntropyService> _entropyServiceMock;
    private readonly EntropyController _controller;

    public EntropyControllerTests()
    {
        _entropyServiceMock = new Mock<IEntropyService>();
        _controller = new EntropyController(_entropyServiceMock.Object);
    }

    [Fact]
    public void CalculateEntropy_ReturnsBadRequest_WhenInputIsNull()
    {
        var request = new EntropyRequest { Input = null };

        var result = _controller.CalculateEntropy(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void CalculateEntropy_ReturnsBadRequest_WhenInputIsWhitespace()
    {
        var request = new EntropyRequest { Input = "   " };

        var result = _controller.CalculateEntropy(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void CalculateEntropy_ReturnsOk_WithExpectedEntropyResponse()
    {
        var request = new EntropyRequest { Input = "testinput" };

        _entropyServiceMock.Setup(s => s.CalculateShannonEntropy(request.Input)).Returns(3.5);
        _entropyServiceMock.Setup(s => s.CalculateMinEntropy(request.Input)).Returns(2.1);
        _entropyServiceMock.Setup(s => s.CalculateRenyiEntropy(request.Input, 2)).Returns(2.8);
        _entropyServiceMock.Setup(s => s.CalculateDistribution(request.Input)).Returns(
            new List<DistributionItem>
            {
                new DistributionItem { Char = "t", Count = 3 },
                new DistributionItem { Char = "e", Count = 1 },
                new DistributionItem { Char = "s", Count = 1 },
                new DistributionItem { Char = "i", Count = 1 },
                new DistributionItem { Char = "n", Count = 1 },
                new DistributionItem { Char = "p", Count = 1 },
                new DistributionItem { Char = "u", Count = 1 },
            });

        var result = _controller.CalculateEntropy(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<EntropyResponse>(okResult.Value);

        Assert.Equal(3.5, response.ShannonEntropy);
        Assert.Equal(2.1, response.MinEntropy);
        Assert.Equal(2.8, response.RenyiEntropy);

        Assert.NotNull(response.Distribution);
        Assert.Contains(response.Distribution, d => d.Char == "t" && d.Count == 3);
    }
}