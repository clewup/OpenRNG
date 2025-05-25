using OpenRNG.Api.Models;
using OpenRNG.Api.Services.Interfaces;

namespace OpenRNG.Api.Services;

public class EntropyService : IEntropyService
{
    public double CalculateShannonEntropy(string input)
    {
        var frequency = new Dictionary<char, int>();

        foreach (var c in input)
        {
            if (!frequency.ContainsKey(c))
                frequency[c] = 0;
            frequency[c]++;
        }

        double entropy = 0;
        double length = input.Length;

        foreach (var kv in frequency)
        {
            double p = kv.Value / length;
            entropy -= p * Math.Log2(p);
        }

        return entropy;
    }
    
    public double CalculateMinEntropy(string input)
    {
        var freq = new Dictionary<char, int>();
        foreach (var c in input)
            freq[c] = freq.GetValueOrDefault(c, 0) + 1;

        double maxProb = freq.Values.Max() / (double)input.Length;
        return -Math.Log2(maxProb);
    }
    
    public double CalculateRenyiEntropy(string input, double alpha = 2)
    {
        if (alpha <= 0 || alpha == 1) 
            throw new ArgumentException("Alpha must be > 0 and != 1");

        var freq = new Dictionary<char, int>();
        foreach (var c in input)
            freq[c] = freq.GetValueOrDefault(c, 0) + 1;

        double sum = 0;
        foreach(var count in freq.Values)
        {
            double p = count / (double)input.Length;
            sum += Math.Pow(p, alpha);
        }

        return 1.0 / (1 - alpha) * Math.Log2(sum);
    }

    public List<DistributionItem> CalculateDistribution(string input)
    {
        var frequency = new Dictionary<char, int>();
        foreach (var c in input)
        {
            if (!frequency.ContainsKey(c))
                frequency[c] = 0;
            frequency[c]++;
        }

        return frequency.Select(kv => new DistributionItem {
            Char = kv.Key.ToString(),
            Count = kv.Value
        }).ToList();
    }
}