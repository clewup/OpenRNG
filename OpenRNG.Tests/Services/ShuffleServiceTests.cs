using OpenRNG.Core.Services;

public class ShuffleServiceTests
{
    private readonly ShuffleService _service = new();

    [Fact]
    public void Shuffle_EmptyList_RemainsEmpty()
    {
        var list = new List<int>();
        _service.Shuffle(list);
        Assert.Empty(list);
    }

    [Fact]
    public void Shuffle_SingleElementList_RemainsSame()
    {
        var list = new List<int> { 42 };
        _service.Shuffle(list);
        Assert.Single(list);
        Assert.Equal(42, list[0]);
    }

    [Fact]
    public void Shuffle_ListElementsArePreserved()
    {
        var list = Enumerable.Range(1, 10).ToList();
        var original = list.ToList();

        _service.Shuffle(list);

        Assert.Equal(original.OrderBy(x => x), list.OrderBy(x => x));
    }

    [Fact]
    public void Shuffle_ShufflesOrder_MostOfTheTime()
    {
        var list = Enumerable.Range(1, 10).ToList();
        var original = list.ToList();

        bool isDifferent = false;

        for (int i = 0; i < 10; i++)
        {
            _service.Shuffle(list);

            if (!list.SequenceEqual(original))
            {
                isDifferent = true;
                break;
            }
        }

        Assert.True(isDifferent, "Shuffled list order should differ from original at least once in multiple tries.");
    }
}