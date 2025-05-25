using Microsoft.Extensions.Caching.Memory;
using OpenRNG.Api.Services;

public class RandomServiceTests
{
    private readonly RandomService _service;

    public RandomServiceTests()
    {
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        _service = new RandomService(memoryCache);
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(0, 0)]
    [InlineData(-5, 5)]
    public void GetSecureRandomInt_ReturnsValueInRange(int min, int max)
    {
        int val = _service.GetSecureRandomInt(min, max);
        Assert.InRange(val, min, max);
    }

    [Fact]
    public void GetSecureRandomPassword_IncludesSymbols_WhenRequested()
    {
        string pwd = _service.GetSecureRandomPassword(20, true);
        Assert.Equal(20, pwd.Length);
        Assert.Matches("[!@#$%^&*()\\-_=+\\[\\]{}|;:,.<>?]", pwd);
    }

    [Fact]
    public void GetSecureRandomPassword_ExcludesSymbols_WhenNotRequested()
    {
        string pwd = _service.GetSecureRandomPassword(20, false);
        Assert.Equal(20, pwd.Length);
        Assert.DoesNotMatch("[!@#$%^&*()\\-_=+\\[\\]{}|;:,.<>?]", pwd);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(1)]
    public void GenerateLoremIpsum_ReturnsExpectedWordCount(int wordCount)
    {
        string result = _service.GenerateLoremIpsum(wordCount);
        var words = result.TrimEnd('.').Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(wordCount, words.Length);
        Assert.True(char.IsUpper(words[0][0]), "First word should start with uppercase");
        Assert.EndsWith(".", result);
    }

    [Fact]
    public void GenerateAvatarUrl_ReturnsValidUrl_AndCaches()
    {
        string seed = "TestSeed";
        string url1 = _service.GenerateAvatarUrl(seed);
        string url2 = _service.GenerateAvatarUrl(seed);

        Assert.Contains("dicebear.com", url1);
        Assert.Equal(url1, url2);
    }

    [Fact]
    public void GetRandomHexColor_ReturnsValidHex()
    {
        string color = _service.GetRandomHexColor();
        Assert.Matches("^#[0-9A-F]{6}$", color);
    }
}
