
using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Infrastructure.Data;
using DatalagringOskarVidell.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class DeltagareRepository(DeltagareDBContext context) : IDeltagareRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<DeltagareDto> CreateAsync(CreateDeltagareDto DeltagareRequest, CancellationToken Ctoken)
    {

        var Entity = new DeltagareEntity
        {
            Fornamn = DeltagareRequest.Firstname,
            Mellannamn = DeltagareRequest.Middlename,
            Efternamn = DeltagareRequest.Lastname,
            Email = DeltagareRequest.Email,
            Telefonnummer = DeltagareRequest.Phonenumber,
        };


            _context.Deltagare_Entity.Add(Entity);
            await _context.SaveChangesAsync(Ctoken);

            return new DeltagareDto(Entity.Id, Entity.Fornamn, Entity.Mellannamn, Entity.Efternamn, Entity.Email, Entity.Telefonnummer);

    }


    public async Task<bool> DeleteAsync(string email, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var entity = await _context.Deltagare_Entity.SingleOrDefaultAsync(e => e.Email == email, Ctoken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Deltagare with email {email} not found");
        }

        _context.Deltagare_Entity.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }


    public async Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken)
    {
       var entities = await _context.Deltagare_Entity
            .AsNoTracking()
            .OrderBy(e => e.Efternamn)
            .ThenBy(e => e.Fornamn)
            .Select(e => new DeltagareDto
            (
                e.Id,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Email,
                e.Telefonnummer
            ))
            .ToListAsync(Ctoken);

        return entities;
    }


    public async Task<DeltagareDto?> GetByEmailAsync(string email, CancellationToken Ctoken)
    {
     if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }
        var deltagare = await _context.Deltagare_Entity
            .AsNoTracking()
            .Select(e => new DeltagareDto
            (
                e.Id,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Email,
                e.Telefonnummer
            ))
            .SingleOrDefaultAsync(e => e.Email == email, Ctoken);
        return deltagare is null ? null : deltagare;
    }

    public async Task<DeltagareDto?> UpdateAsync(string email, UpdateDeltagareDto DeltagareRequest, CancellationToken Ctoken)
    {
        if (DeltagareRequest.Email == string.Empty)
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }
        var entity = await _context.Deltagare_Entity.SingleOrDefaultAsync(e => e.Email == DeltagareRequest.Email, Ctoken)
            ?? throw new KeyNotFoundException($"Deltagare with email {email} not found");

        entity.Email = DeltagareRequest.Email;
        entity.Fornamn = DeltagareRequest.Firstname;
        entity.Mellannamn = DeltagareRequest.Middlename;
        entity.Efternamn = DeltagareRequest.Lastname;
        entity.Telefonnummer = DeltagareRequest.Phonenumber;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.Deltagare_Entity
            .AsNoTracking()
            .Where(e => e.Email == email)
            .Select(e => new DeltagareDto
            (
                e.Id,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Email,
                e.Telefonnummer
            ))
            .SingleOrDefaultAsync(e => e.Email == email, Ctoken);
    }
}
