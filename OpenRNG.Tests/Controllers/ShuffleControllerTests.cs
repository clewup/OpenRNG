using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenRNG.Api.Controllers;
using OpenRNG.Api.Models;
using OpenRNG.Core.Services.Interfaces;

namespace OpenRNG.Tests.Controllers;

public class ShuffleControllerTests
{
    private readonly Mock<IShuffleService> _mockShuffleService;
    private readonly ShuffleController _controller;

    public ShuffleControllerTests()
    {
        _mockShuffleService = new Mock<IShuffleService>();
        _controller = new ShuffleController(_mockShuffleService.Object);
    }

    [Fact]
    public void ShuffleList_ReturnsBadRequest_WhenItemsIsNull()
    {
        var result = _controller.ShuffleList(null);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var obj = badRequest.Value;
        Assert.NotNull(obj);
    }

    [Fact]
    public void ShuffleList_ReturnsBadRequest_WhenItemsIsEmpty()
    {
        var result = _controller.ShuffleList(new List<object>());

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var obj = badRequest.Value;
        Assert.NotNull(obj);
    }

    [Fact]
    public void ShuffleList_CallsShuffleService_AndReturnsOkResult()
    {
        var items = new List<object> { 1, 2, 3 };
        _mockShuffleService.Setup(s => s.Shuffle(items)).Callback<IList<object>>(list =>
        {
            for (int i = 0, j = list.Count - 1; i < j; i++, j--)
            {
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        });

        var result = _controller.ShuffleList(items);

        _mockShuffleService.Verify(s => s.Shuffle(items), Times.Once);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ShuffleResponse>(okResult.Value);

        Assert.NotNull(response.Shuffled);
        Assert.Equal(items.Count, response.Shuffled.Count);
        Assert.Equal(items, response.Shuffled);
    }
}