using OpenRNG.Api.Models;

namespace OpenRNG.Api.Services.Interfaces;

public interface IEntropyService
{
    double CalculateShannonEntropy(string input);
    double CalculateMinEntropy(string input);
    double CalculateRenyiEntropy(string input, double alpha = 2);
    List<DistributionItem> CalculateDistribution(string input);
}