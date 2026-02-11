using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Domain.Models.KursRegi;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IDeltagareRepository
{
    Task<Guid> CreateAsync(CreateDeltagareDto DeltagareRequest, CancellationToken Ctoken);

    Task<DeltagareDto?> GetByIDAsync(Guid ID, CancellationToken Ctoken);
    Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken);

    Task<DeltagareDto?> UpdateAsync(Guid id, UpdateDeltagareDto DeltagareRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken);
}
