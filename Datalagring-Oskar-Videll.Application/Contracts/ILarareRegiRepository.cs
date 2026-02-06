

using DatalagringOskarVidell.Domain.Models.KursRegi.LarareRegi;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;

namespace DatalagringOskarVidell.Application.Contracts;

public interface ILarareRegiRepository
{
    Task<LarareRegiDto> CreateAsync(CreateLarareRegiDto LarareRegiRequest, CancellationToken Ctoken);
    Task<LarareRegiDto?> GetByIdAsync(int Id, CancellationToken Ctoken);
    Task<IReadOnlyList<LarareRegiDto>> GetAllAsync(CancellationToken Ctoken);
    Task<LarareRegiDto?> UpdateAsync(int Id, UpdateLarareRegiDto KurstillfalleRequest, CancellationToken Ctoken);
    Task<bool> DeleteAsync(int Id, CancellationToken Ctoken);
}
