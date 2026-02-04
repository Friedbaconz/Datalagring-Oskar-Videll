

using DatalagringOskarVidell.Domain.Models.KursTillfallen;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IKursTillfalleRepository
{
    Task<KurstillfalleDto> CreateAsync(CreateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<KurstillfalleDto?> GetByIdAsync(Guid kursTillfallenId, CancellationToken Ctoken);
    Task<IReadOnlyList<KurstillfalleDto>> GetAllAsync(CancellationToken Ctoken);
    Task<KurstillfalleDto?> UpdateAsync(Guid kursTillfallenId, UpdateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(Guid kursTillfallenId, CancellationToken Ctoken);
}
