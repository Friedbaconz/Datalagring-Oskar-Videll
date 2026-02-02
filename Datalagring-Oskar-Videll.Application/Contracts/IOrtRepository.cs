
using Datalagring_Oskar_Videll.Domain.Models.Ort;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface IOrtRepository
{
    Task<OrtDto> CreateAsync(CreateOrtDto OrtRequest, CancellationToken Ctoken);
    Task<OrtDto?> GetByIdAsync(Guid ortId, CancellationToken Ctoken);
    Task<IReadOnlyList<OrtDto>> GetAllAsync(CancellationToken Ctoken);
    Task<OrtDto?> UpdateAsync(Guid ortId, UpdateOrtDto OrtRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(Guid ortId, CancellationToken Ctoken);
}
