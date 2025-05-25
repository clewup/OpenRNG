using System.Security.Cryptography;
using OpenRNG.Core.Services.Interfaces;

namespace OpenRNG.Core.Services;

public class ShuffleService : IShuffleService
{
    public void Shuffle<T>(IList<T> list)
    {
        var rng = RandomNumberGenerator.Create();

        int n = list.Count;
        while (n > 1)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int k = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
            k %= n;

            n--;
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}