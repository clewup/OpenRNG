namespace OpenRNG.Core.Services.Interfaces;

public interface IShuffleService
{
    void Shuffle<T>(IList<T> list);
}