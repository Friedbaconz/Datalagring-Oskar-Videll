using DatalagringOskarVidell.Domain.Models.Deltagare;

namespace DatalagringOskarVidell.Application.Contracts;

public interface IDeltagareRepository
{
    Task<DeltagareDto> CreateAsync(CreateDeltagareDto DeltagareRequest, CancellationToken Ctoken);

    Task<DeltagareDto?> GetByIDAsync(Guid ID, CancellationToken Ctoken);
    Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken);

    Task<DeltagareDto?> UpdateAsync(string email, UpdateDeltagareDto DeltagareRequest, CancellationToken Ctoken);

    Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken);
}
