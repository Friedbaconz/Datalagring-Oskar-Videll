
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Domain.Models.Kurs;
using DatalagringOskarVidell.Domain.Models.Larare;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IKursRepository
{
    Task<KursDto> CreateAsync(CreateKursDto KursRequest, CancellationToken Ctoken);

    Task<KursDto?> GetByKursAsync(string Kurskod, CancellationToken Ctoken);
    Task<IReadOnlyList<KursDto>> GetAllAsync(CancellationToken Ctoken);

    Task<KursDto?> UpdateAsync(string Kurskod, UpdateKursDto KursRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(string Kurskod, CancellationToken Ctoken);
}
