
using Datalagring_Oskar_Videll.Application.Contracts;
using Datalagring_Oskar_Videll.Domain.Models.Deltagare;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Datalagring_Oskar_Videll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Repository;

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
            StatusTypeId = 1
        };

        try
        {
            _context.Deltagare.Add(Entity);
            await _context.SaveChangesAsync(Ctoken);

            return new DeltagareDto(Entity.Email, Entity.Fornamn, Entity.Mellannamn, Entity.Efternamn, Entity.Telefonnummer);

        }
        catch (Exception ex)
        {
            throw new Exception("Could not create Deltagare", ex);
        }

    }


    public async Task<bool> DeleteAsync(string email, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var entity = await _context.Deltagare.SingleOrDefaultAsync(e => e.Email == email, Ctoken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Deltagare with email {email} not found");
        }

        _context.Deltagare.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }


    public async Task<IReadOnlyList<DeltagareDto>> GetAllAsync(CancellationToken Ctoken)
    {
       var entities = await _context.Deltagare
            .AsNoTracking()
            .OrderBy(e => e.Efternamn)
            .ThenBy(e => e.Fornamn)
            .Select(e => new DeltagareDto
            (
                e.Email,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
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
        var deltagare = await _context.Deltagare
            .AsNoTracking()
            .Select(e => new DeltagareDto
            (
                e.Email,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
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
        var entity = await _context.Deltagare.SingleOrDefaultAsync(e => e.Email == DeltagareRequest.Email, Ctoken)
            ?? throw new KeyNotFoundException($"Deltagare with email {email} not found");

        entity.Email = DeltagareRequest.Email;
        entity.Fornamn = DeltagareRequest.Firstname;
        entity.Mellannamn = DeltagareRequest.Middlename;
        entity.Efternamn = DeltagareRequest.Lastname;
        entity.Telefonnummer = DeltagareRequest.Phonenumber;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.Deltagare
            .AsNoTracking()
            .Where(e => e.Email == email)
            .Select(e => new DeltagareDto
            (
                e.Email,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Telefonnummer
            ))
            .SingleOrDefaultAsync(e => e.Email == email, Ctoken);
    }
}
