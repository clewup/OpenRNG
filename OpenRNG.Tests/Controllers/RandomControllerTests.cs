using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenRNG.Api.Controllers;
using OpenRNG.Api.Services.Interfaces;

namespace OpenRNG.Tests.Controllers;

public class RandomControllerTests
{
    private readonly Mock<IRandomService> _randomServiceMock;
    private readonly RandomController _controller;

    public RandomControllerTests()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _controller = new RandomController(_randomServiceMock.Object);
    }

    [Fact]
    public void GetRandomInteger_ReturnsBadRequest_WhenMinIsNotLessThanMax()
    {
        var result = _controller.GetRandomInteger(min: 10, max: 5);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetRandomInteger_ReturnsOk_WithRandomInteger()
    {
        _randomServiceMock.Setup(s => s.GetSecureRandomInt(0, 10)).Returns(7);

        var result = _controller.GetRandomInteger(min: 0, max: 10);
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = JsonSerializer.Serialize(okResult.Value);
        Assert.Contains("\"Type\":\"integer\"", json);
        Assert.Contains("\"Value\":7", json);
    }

    [Fact]
    public void GetRandomPassword_ReturnsBadRequest_WhenLengthIsZeroOrLess()
    {
        var result = _controller.GetRandomPassword(length: 0);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetRandomPassword_ReturnsOk_WithPassword()
    {
        _randomServiceMock.Setup(s => s.GetSecureRandomPassword(12, false)).Returns("abc123");

        var result = _controller.GetRandomPassword(length: 12, includeSymbols: false);
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = JsonSerializer.Serialize(okResult.Value);
        Assert.Contains("\"Type\":\"password\"", json);
        Assert.Contains("\"Value\":\"abc123\"", json);
    }

    [Fact]
    public void GetRandomLoremIpsum_ReturnsBadRequest_WhenWordsIsZeroOrLess()
    {
        var result = _controller.GetRandomLoremIpsum(words: 0);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetRandomLoremIpsum_ReturnsOk_WithLoremText()
    {
        _randomServiceMock.Setup(s => s.GenerateLoremIpsum(10)).Returns("Lorem ipsum dolor sit amet.");

        var result = _controller.GetRandomLoremIpsum(words: 10);
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = JsonSerializer.Serialize(okResult.Value);
        Assert.Contains("\"Type\":\"lorem\"", json);
        Assert.Contains("\"Value\":\"Lorem ipsum dolor sit amet.\"", json);
    }

    [Fact]
    public void GetRandomAvatar_ReturnsBadRequest_WhenSeedIsNullOrWhitespace()
    {
        var result = _controller.GetRandomAvatar(seed: "  ");

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetRandomAvatar_ReturnsOk_WithAvatarUrl()
    {
        _randomServiceMock.Setup(s => s.GenerateAvatarUrl("seed", "initials"))
            .Returns("https://api.dicebear.com/9.x/initials/svg?seed=seed");

        var result = _controller.GetRandomAvatar(seed: "seed", style: "initials");
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = JsonSerializer.Serialize(okResult.Value);
        Assert.Contains("\"Type\":\"avatar\"", json);
        Assert.Contains("\"Value\":\"https://api.dicebear.com/9.x/initials/svg?seed=seed\"", json);
    }

    [Fact]
    public void GetRandomColor_ReturnsOk_WithColor()
    {
        _randomServiceMock.Setup(s => s.GetRandomHexColor()).Returns("#FF00FF");

        var result = _controller.GetRandomColor();
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = JsonSerializer.Serialize(okResult.Value);
        Assert.Contains("\"Type\":\"color\"", json);
        Assert.Contains("\"Value\":\"#FF00FF\"", json);
    }
}