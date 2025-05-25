using OpenRNG.Api.Models;

namespace OpenRNG.Api.Services.Interfaces;

public interface IShuffleService
{
    void Shuffle<T>(IList<T> list);
}