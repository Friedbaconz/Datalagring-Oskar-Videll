
using Datalagring_Oskar_Videll.Application.Contracts;
using Datalagring_Oskar_Videll.Domain.Entities;
using Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Repository;

public class KurstillfalleRepository(DeltagareDBContext context) : IKursTillfalleRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<KurstillfalleDto> CreateAsync(CreateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken)
    {
        var entity = new Kurstillfalle_Entity
        {
            Startdatum = KurstillfalleRequest.Startdatum,
            Slutdatum = KurstillfalleRequest.Slutdatum,
            MaxSeats = KurstillfalleRequest.Maxseats,
        };

        try 
        {
            _context.KursTillfalle.Add(entity);
            await _context.SaveChangesAsync(Ctoken);

            return new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.LarareEmail,
                entity.Ortid
            );

        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the Kurstillfalle.", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid kursTillfallenId, CancellationToken Ctoken)
    {
        if (kursTillfallenId == Guid.Empty)
        {
            throw new ArgumentException("kursTillfallenId cannot be empty", nameof(kursTillfallenId));
        }

        var entity = _context.KursTillfalle.SingleOrDefault(e => e.KursTillfallenId == kursTillfallenId);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Kurstillfalle with ID {kursTillfallenId} not found.");
        }

        _context.KursTillfalle.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }

    public async Task<IReadOnlyList<KurstillfalleDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _context.KursTillfalle
            .AsNoTracking()
            .Select(entity => new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.LarareEmail,
                entity.Ortid
            ))
            .ToListAsync(Ctoken);

        return entities;
    }

    public async Task<KurstillfalleDto?> GetByIdAsync(Guid kursTillfallenId, CancellationToken Ctoken)
    {
        if (kursTillfallenId == Guid.Empty)
        {
            throw new ArgumentException("kursTillfallenId cannot be empty", nameof(kursTillfallenId));
        }

        var kurstillfalle = await _context.KursTillfalle
            .AsNoTracking()
            .Select(entity => new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.LarareEmail,
                entity.Ortid
            ))
            .SingleOrDefaultAsync(e => e.KursTillfallenId == kursTillfallenId, Ctoken);

        return kurstillfalle is null ? null : kurstillfalle;
    }

    public async Task<KurstillfalleDto?> UpdateAsync(Guid kursTillfallenId, UpdateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken)
    {
        if (kursTillfallenId == Guid.Empty)
        {
            throw new ArgumentException("kursTillfallenId cannot be empty", nameof(kursTillfallenId));
        }

        var entity = _context.KursTillfalle.SingleOrDefault(e => e.KursTillfallenId == kursTillfallenId)
            ?? throw new KeyNotFoundException($"Kurstillfalle with ID {kursTillfallenId} not found.");

        entity.KursTillfallenId = KurstillfalleRequest.KursTillfallenId;
        entity.KursKod = KurstillfalleRequest.KursKod;
        entity.Startdatum = KurstillfalleRequest.Startdatum;
        entity.Slutdatum = KurstillfalleRequest.Slutdatum;
        entity.MaxSeats = KurstillfalleRequest.Maxseats;
        entity.Ortid = KurstillfalleRequest.Ortid;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursTillfalle
            .AsNoTracking()
            .Select(entity => new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.LarareEmail,
                entity.Ortid
            ))
            .SingleOrDefaultAsync(e => e.KursTillfallenId == kursTillfallenId, Ctoken);



    }
}
