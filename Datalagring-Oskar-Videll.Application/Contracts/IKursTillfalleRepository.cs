

using Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface IKursTillfalleRepository
{
    Task<KurstillfalleDto> CreateAsync(CreateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<KurstillfalleDto?> GetByIdAsync(Guid kursTillfallenId, CancellationToken Ctoken);
    Task<IReadOnlyList<KurstillfalleDto>> GetAllAsync(CancellationToken Ctoken);
    Task<KurstillfalleDto?> UpdateAsync(Guid kursTillfallenId, UpdateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(Guid kursTillfallenId, CancellationToken Ctoken);
}
