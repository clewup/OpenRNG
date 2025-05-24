namespace OpenRNG.Api.Services.Interfaces;

public interface IRandomService
{
    int GetSecureRandomInt(int min, int max);
    string GetSecureRandomPassword(int length, bool includeSymbols);
    string GenerateLoremIpsum(int wordCount);
    string GenerateAvatarUrl(string seed, string style = "identicon");
    string GetRandomHexColor();
}