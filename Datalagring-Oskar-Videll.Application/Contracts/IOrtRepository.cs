
using DatalagringOskarVidell.Domain.Models.Ort;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IOrtRepository
{
    Task<OrtDto> CreateAsync(CreateOrtDto OrtRequest, CancellationToken Ctoken);
    Task<OrtDto?> GetByIdAsync(int ortId, CancellationToken Ctoken);
    Task<IReadOnlyList<OrtDto>> GetAllAsync(CancellationToken Ctoken);
    Task<OrtDto?> UpdateAsync(int ortId, UpdateOrtDto OrtRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(int ortId, CancellationToken Ctoken);
}
