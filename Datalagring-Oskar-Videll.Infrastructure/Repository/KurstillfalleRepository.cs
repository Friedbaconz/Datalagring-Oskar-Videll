
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
            KursKod = KurstillfalleRequest.KursKod,
            Kurs = KurstillfalleRequest.kurs,
            Startdatum = KurstillfalleRequest.Startdatum,
            Slutdatum = KurstillfalleRequest.Slutdatum,
            MaxSeats = KurstillfalleRequest.Maxseats,
            Ortid = KurstillfalleRequest.Ortid,
            Ort = KurstillfalleRequest.Ort,
            KursTillfallenLarare = KurstillfalleRequest.LarareTillfallenId,
            KursRegi = KurstillfalleRequest.KursrgisteringsId
        };

        try 
        {
            _context.KursTillfalle.Add(entity);
            await _context.SaveChangesAsync(Ctoken);

            return new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.KursTillfallenLarare.FirstOrDefault()?.LarareEmail ?? string.Empty,
                entity.Ortid,
                entity.Ort,
                entity.KursRegi,
                entity.KursTillfallenLarare
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
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                string.Empty,
                entity.Ortid,
                entity.Ort,
                entity.KursRegi,
                entity.KursTillfallenLarare
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

        var entity = await _context.KursTillfalle
            .AsNoTracking()
            .Select(entity => new KurstillfalleDto
            (
                entity.KursTillfallenId,
                entity.KursKod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                string.Empty,
                entity.Ortid,
                entity.Ort,
                entity.KursRegi,
                entity.KursTillfallenLarare
            ))
            .SingleOrDefaultAsync(e => e.KursTillfallenId == kursTillfallenId, Ctoken);

        return entity;
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
        entity.KursRegi = KurstillfalleRequest.KursrgisteringsId;
        entity.KursTillfallenLarare = KurstillfalleRequest.LarareTillfallenId;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursTillfalle
            .AsNoTracking()
            .Select(e => new KurstillfalleDto
            (
                e.KursTillfallenId,
                e.KursKod,
                e.Kurs,
                e.Startdatum,
                e.Slutdatum,
                e.MaxSeats,
                string.Empty,
                e.Ortid,
                e.Ort,
                e.KursRegi,
                e.KursTillfallenLarare
            ))
            .SingleOrDefaultAsync(e => e.KursTillfallenId == kursTillfallenId, Ctoken);



    }
}
