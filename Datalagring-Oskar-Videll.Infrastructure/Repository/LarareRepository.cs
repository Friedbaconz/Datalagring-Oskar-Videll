
using Datalagring_Oskar_Videll.Application.Contracts;
using Datalagring_Oskar_Videll.Domain.Entities;
using Datalagring_Oskar_Videll.Domain.Models.Deltagare;
using Datalagring_Oskar_Videll.Domain.Models.Larare;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Dapper.SqlMapper;

namespace Datalagring_Oskar_Videll.Infrastructure.Repository;

public class LarareRepository(DeltagareDBContext context) : ILarareRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<LarareDto> CreateAsync(CreateLarareDto LarareRequest, CancellationToken Ctoken)
    {
        var entity = new Larare_Entity
        {
            LarareEmail = LarareRequest.Email,
            Fornamn = LarareRequest.Firstname,
            Mellannamn = LarareRequest.Middlename!,
            Efternamn = LarareRequest.Lastname,
            Kompentens = LarareRequest.Kompentens
        };

        try
        {
            _context.Larare.Add(entity);
            await _context.SaveChangesAsync(Ctoken);

            return new LarareDto(Email: entity.LarareEmail,
                                 Firstname: entity.Fornamn,
                                 Middlename: entity.Mellannamn,
                                 Lastname: entity.Efternamn,
                                 Kompentens: entity.Kompentens);

        }
        catch (Exception ex)
        {
            throw new Exception("Could not create Larare", ex);
        }

    }

    public async Task<bool> DeleteAsync(string email, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var entity = _context.Larare.SingleOrDefault(e => e.LarareEmail == email);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Larare with email {email} not found");
        }

        _context.Larare.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;

    }

    public async Task<IReadOnlyList<LarareDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _context.Larare
            .AsNoTracking()
            .OrderBy(e => e.Efternamn)
            .ThenBy(e => e.Fornamn)
            .Select(e => new LarareDto
            (
                e.LarareEmail,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Kompentens
            ))
            .ToListAsync(Ctoken);

        return entities;

    }

    public async Task<LarareDto?> GetByEmailAsync(string email, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var Larare = await _context.Larare
            .AsNoTracking()
            .Select(e => new LarareDto
            (
                e.LarareEmail,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Kompentens
            ))
            .SingleOrDefaultAsync(e => e.Email == email, Ctoken);

        return Larare is null ? null : Larare;
    }

    public async Task<LarareDto?> UpdateAsync(string email, UpdateLarareDto LarareRequest, CancellationToken Ctoken)
    {
        if (LarareRequest.Email == string.Empty)
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var entity = await _context.Larare.SingleOrDefaultAsync(e => e.LarareEmail == LarareRequest.Email, Ctoken)
            ?? throw new KeyNotFoundException($"Larare with email {email} not found");

        entity.LarareEmail = LarareRequest.Email;
        entity.Fornamn = LarareRequest.Firstname;
        entity.Mellannamn = LarareRequest.Middlename!;
        entity.Efternamn = LarareRequest.Lastname;
        entity.Kompentens = LarareRequest.Kompentens;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.Larare
            .AsNoTracking()
            .Where(e => e.LarareEmail == email)
            .Select(e => new LarareDto
            (
                e.LarareEmail,
                e.Fornamn,
                e.Mellannamn,
                e.Efternamn,
                e.Kompentens
            ))
            .SingleOrDefaultAsync(e => e.Email == email, Ctoken);
    }
}
