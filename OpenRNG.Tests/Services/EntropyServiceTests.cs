using OpenRNG.Api.Services;

public class EntropyServiceTests
{
    private readonly EntropyService _service = new();

    [Fact]
    public void CalculateShannonEntropy_EmptyString_ReturnsZero()
    {
        double entropy = _service.CalculateShannonEntropy("");
        Assert.Equal(0, entropy);
    }

    [Fact]
    public void CalculateShannonEntropy_KnownValue_ReturnsExpected()
    {
        Assert.Equal(0, _service.CalculateShannonEntropy("aaaa"));

        double entropy = _service.CalculateShannonEntropy("abab");
        Assert.InRange(entropy, 0.99, 1.01);
    }

    [Fact]
    public void CalculateMinEntropy_EmptyString_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => _service.CalculateMinEntropy(""));
    }

    [Fact]
    public void CalculateMinEntropy_KnownValue_ReturnsExpected()
    {
        Assert.Equal(0, _service.CalculateMinEntropy("aaaa"));

        double entropy = _service.CalculateMinEntropy("aaab");
        Assert.InRange(entropy, 0.41, 0.42);
    }

    [Fact]
    public void CalculateRenyiEntropy_InvalidAlpha_Throws()
    {
        Assert.Throws<ArgumentException>(() => _service.CalculateRenyiEntropy("test", 1));
        Assert.Throws<ArgumentException>(() => _service.CalculateRenyiEntropy("test", 0));
        Assert.Throws<ArgumentException>(() => _service.CalculateRenyiEntropy("test", -1));
    }

    [Fact]
    public void CalculateRenyiEntropy_KnownValue_DefaultAlpha2()
    {
        var input = "aabb";
        double entropy = _service.CalculateRenyiEntropy(input);
        Assert.InRange(entropy, 0.9, 1.1);
    }

    [Fact]
    public void CalculateDistribution_ReturnsCorrectFrequencies()
    {
        var input = "aabbccaa";
        var distribution = _service.CalculateDistribution(input);

        Assert.Equal(4, distribution.Single(x => x.Char == "a").Count);
        Assert.Equal(2, distribution.Single(x => x.Char == "b").Count);
        Assert.Equal(2, distribution.Single(x => x.Char == "c").Count);
    }
}
