using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using OpenRNG.Api.Services.Interfaces;

namespace OpenRNG.Api.Services;

public class RandomService(IMemoryCache memoryCache) : IRandomService
{
    private static readonly char[] Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private static readonly char[] Digits = "0123456789".ToCharArray();
    private static readonly char[] Symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?".ToCharArray();
    
    private static readonly string[] LoremWords = new[]
    {
        "lorem", "ipsum", "dolor", "sit", "amet", "consectetur",
        "adipiscing", "elit", "sed", "do", "eiusmod", "tempor",
        "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua"
    };
    
    public int GetSecureRandomInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max + 1);
    }

    public string GetSecureRandomPassword(int length, bool includeSymbols)
    {
        var characterSet = new List<char>();
        characterSet.AddRange(Letters);
        characterSet.AddRange(Digits);
        
        if (includeSymbols)
            characterSet.AddRange(Symbols);

        var passwordChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            passwordChars[i] = characterSet[GetSecureRandomInt(0, characterSet.Count - 1)];
        }

        return new string(passwordChars);
    }

    public string GenerateLoremIpsum(int wordCount = 10)
    {
        string cacheKey = $"lorem_{wordCount}";
        
        return memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

            var words = new List<string>(wordCount);

            for (int i = 0; i < wordCount; i++)
            {
                int index = RandomNumberGenerator.GetInt32(0, LoremWords.Length);
                words.Add(LoremWords[index]);
            }

            if (words.Count > 0)
            {
                words[0] = char.ToUpper(words[0][0]) + words[0].Substring(1);
                words[words.Count - 1] += ".";
            }

            return string.Join(" ", words);
        })!;
    }
    
    public string GenerateAvatarUrl(string seed, string style = "initials")
    {
        var encodedSeed = Uri.EscapeDataString(seed.Trim().ToLower());
        string cacheKey = $"avatar_{style}_{encodedSeed}";
        
        return memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            
            var avatarUrl = $"https://api.dicebear.com/9.x/{style}/svg?seed={encodedSeed}";

            return avatarUrl;
        })!;
    }
    
    public string GetRandomHexColor()
    {
        byte[] rgb = new byte[3];
        RandomNumberGenerator.Fill(rgb);

        return $"#{rgb[0]:X2}{rgb[1]:X2}{rgb[2]:X2}";
    }
}