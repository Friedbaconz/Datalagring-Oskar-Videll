

using Datalagring_Oskar_Videll.Domain.Models.KursRegi.LarareRegi;
using Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface ILarareRegiRepository
{
    Task<LarareRegiDto> CreateAsync(CreateLarareRegiDto LarareRegiRequest, CancellationToken Ctoken);
    Task<LarareRegiDto?> GetByIdAsync(Guid Id, CancellationToken Ctoken);
    Task<IReadOnlyList<LarareRegiDto>> GetAllAsync(CancellationToken Ctoken);
    Task<LarareRegiDto?> UpdateAsync(Guid Id, UpdateLarareRegiDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken);
}
