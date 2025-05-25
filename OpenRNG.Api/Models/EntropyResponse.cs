using OpenRNG.Core.Models;

namespace OpenRNG.Api.Models;

public class EntropyResponse
{
    public double ShannonEntropy { get; set; }
    public double MinEntropy { get; set; }
    public double RenyiEntropy { get; set; }
    public List<DistributionItem> Distribution { get; set; }
}