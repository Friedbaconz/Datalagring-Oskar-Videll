

namespace Datalagring_Oskar_Videll.Application.Interfaces;
using Datalagring_Oskar_Videll.Domain.Models;
public interface IDeltagare_Repository
{
    Task<Deltagare> CreateDeltagareAsync(CreateDeltagareDto deltagare, CancellationToken cToken = default);
    Task<bool> DeleteDeltagareAsync(string email, CancellationToken cToken);
    Task<IReadOnlyList<Deltagare>> GetAllAsync(CancellationToken cToken = default);
    Task<Deltagare?> GetDeltagareByEmailAsync(string email, CancellationToken cToken = default);
    Task<Deltagare?> UpdateDeltagareAsync(UpdateDeltagareDto deltagare, CancellationToken cToken);
}
