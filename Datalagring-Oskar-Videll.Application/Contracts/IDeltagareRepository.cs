using Datalagring_Oskar_Videll.Domain.Models.Deltagare;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface IDeltagareRepository
{
    Task CreateAsync(CreateDeltagareDto deltagare, CancellationToken Ctoken);
}
