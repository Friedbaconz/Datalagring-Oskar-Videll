
using Datalagring_Oskar_Videll.Domain.Models.Deltagare;
using Datalagring_Oskar_Videll.Domain.Models.Kurs;
using Datalagring_Oskar_Videll.Domain.Models.Larare;

namespace Datalagring_Oskar_Videll.Application.Contracts;

public interface IKursRepository
{
    Task<KursDto> CreateAsync(CreateKursDto KursRequest, CancellationToken Ctoken);

    Task<KursDto?> GetByKursAsync(string Kurskod, CancellationToken Ctoken);
    Task<IReadOnlyList<KursDto>> GetAllAsync(CancellationToken Ctoken);

    Task<KursDto?> UpdateAsync(string Kurskod, UpdateKursDto KursRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(string Kurskod, CancellationToken Ctoken);
}
