
using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Infrastructure.Data;
using DatalagringOskarVidell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DatalagringOskarVidell.Domain.Models.KursRegi;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class DeltagareRepository(DeltagareDBContext context) : IDeltagareRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<Guid> CreateAsync(CreateDeltagareDto DeltagareRequest, CancellationToken Ctoken)
    {

        var entity = new DeltagareEntity
        {
            Fornamn = DeltagareRequest.Firstname.Trim(),
            Mellannamn = DeltagareRequest.Middlename!.Trim(),
            Efternamn = DeltagareRequest.Lastname.Trim(),
            Email = DeltagareRequest.Email.Trim(),
            Telefonnummer = DeltagareRequest.Phonenumber
        };


            _context.Deltagare_Entity.Add(entity);
            await _context.SaveChangesAsync(Ctoken);

        return entity.ID;
    }


    public async Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(Id));
        }

        var entity = await _context.Deltagare_Entity
            .Where(x => x.ID == Id)
            .SingleOrDefaultAsync(Ctoken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Deltagare with email {Id} not found");
        }

        _context.Deltagare_Entity.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }

    public async Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken)
    {
       var entities = await _context.Deltagare_Entity
            .AsNoTracking()
            .OrderBy(x => x.ID)
            .ToListAsync(Ctoken);

        return [.. entities.Select(MapToDomain)];
    }


    public async Task<DeltagareDto?> GetByIDAsync(Guid ID, CancellationToken Ctoken)
    {
     if (ID == Guid.Empty)
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(ID));
        }
        var deltagare = await _context.Deltagare_Entity
            .AsNoTracking()
            .Where(e => e.ID == ID)
            .SingleOrDefaultAsync(Ctoken);

        return deltagare is null ? null : MapToDomain(deltagare);
    }

    public async Task<DeltagareDto?> UpdateAsync(Guid id, UpdateDeltagareDto DeltagareRequest, CancellationToken Ctoken)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("id cannot be null or empty", nameof(id));
        }
        var entity = await _context.Deltagare_Entity.SingleOrDefaultAsync(e => e.ID == id, Ctoken)
            ?? throw new KeyNotFoundException($"Deltagare with id {id} not found");

        entity.Email = DeltagareRequest.Email;
        entity.Fornamn = DeltagareRequest.Firstname;
        entity.Mellannamn = DeltagareRequest.Middlename;
        entity.Efternamn = DeltagareRequest.Lastname;
        entity.Telefonnummer = DeltagareRequest.Phonenumber;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.Deltagare_Entity
            .AsNoTracking()
            .Where(e => e.ID == id)
            .Select(e => new DeltagareDto
            (
                e.ID,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Email,
                e.Telefonnummer,
                e.KursRegiDeltagare
            ))
            .SingleOrDefaultAsync(Ctoken);
    }

    private static DeltagareDto MapToDomain(DeltagareEntity entity) => new DeltagareDto(Id: entity.ID, Firstname: entity.Fornamn, Middlename: entity.Mellannamn, Lastname: entity.Efternamn, Email: entity.Email, Phonenumber: entity.Telefonnummer, Antagnakurser: entity.KursRegiDeltagare);
}
