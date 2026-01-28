using Datalagring_Oskar_Videll.Domain.Models.Deltagare;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface IDeltagareRepository
{
    Task<DeltagareDto> CreateAsync(CreateDeltagareDto DeltagareRequest, CancellationToken Ctoken);
    Task<DeltagareDto?> GetByEmailAsync(string email, CancellationToken Ctoken);
    Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken);

    Task<DeltagareDto?> UpdateAsync(string email, UpdateDeltagareDto DeltagareRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(string email, CancellationToken Ctoken);
}
