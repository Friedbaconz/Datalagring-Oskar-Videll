
using DatalagringOskarVidell.Domain.Models.Kurs;
using DatalagringOskarVidell.Domain.Models.KursRegi;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IKursRegiRepository
{
    Task<KursRegiDto?> CreateAsync(CreateKursRegiDto KursRegiRequest, CancellationToken Ctoken);

    Task<KursRegiDto?> GetByIDAsync(Guid Id, CancellationToken Ctoken);
    Task<IReadOnlyList<KursRegiDto>> GetAllAsync(CancellationToken Ctoken);

    Task<KursRegiDto?> UpdateAsync(Guid Id, UpdateKursRegiDto KursRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken);
}
