
using DatalagringOskarVidell.Domain.Models.Kurs;
using DatalagringOskarVidell.Domain.Models.KursRegi;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IKursRegiRepository
{
    Task<KursRegiDto> CreateAsync(CreateKursRegiDto KursRegiRequest, CancellationToken Ctoken);

    Task<KursRegiDto?> GetByIDAsync(int Id, CancellationToken Ctoken);
    Task<IReadOnlyList<KursRegiDto>> GetAllAsync(CancellationToken Ctoken);

    Task<KursRegiDto?> UpdateAsync(int Id, UpdateKursRegiDto KursRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(int Id, CancellationToken Ctoken);
}
