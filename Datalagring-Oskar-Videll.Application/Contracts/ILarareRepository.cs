
namespace Datalagring_Oskar_Videll.Application.Contracts;

using Datalagring_Oskar_Videll.Domain.Models.Larare;
public interface ILarareRepository
{
    Task<LarareDto> CreateAsync(CreateLarareDto LarareRequest, CancellationToken Ctoken);

    Task<LarareDto?> GetByEmailAsync(string email, CancellationToken Ctoken);
    Task<IReadOnlyList<LarareDto>> GetAllAsync(CancellationToken Ctoken);

    Task<LarareDto?> UpdateAsync(string email, UpdateLarareDto LarareRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(string email, CancellationToken Ctoken);
}
