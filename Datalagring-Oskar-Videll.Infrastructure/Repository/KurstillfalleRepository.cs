
using Dapper;
using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;
using DatalagringOskarVidell.Domain.Models.Larare;
using DatalagringOskarVidell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class KurstillfalleRepository(DeltagareDBContext context) : IKursTillfalleRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<KurstillfalleDto?> CreateAsync(CreateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken)
    {
        var entity = new Kurstillfalle_Entity
        {
            ID = Guid.NewGuid(),
            KursKodID = KurstillfalleRequest.Kurskod,
            Startdatum = KurstillfalleRequest.Startdatum,
            Slutdatum = KurstillfalleRequest.Slutdatum,
            MaxSeats = KurstillfalleRequest.Maxseats,
            Kurs = _context.Kurs.FirstOrDefault(e => e.Kurskod == KurstillfalleRequest.Kurskod),
            Ortid = KurstillfalleRequest.OrtId,
            Ort = _context.Ort.FirstOrDefault(e => e.OrtId == KurstillfalleRequest.OrtId),
        };

        _context.KursTillfalle.Add(entity);
        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursTillfalle
            .AsNoTracking()
            .Where(e => e.ID == entity.ID)
            .Select(entity => new KurstillfalleDto(
                entity.ID,
                entity.Kurs.Kurskod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.Ort.OrtId,
                entity.Ort
                ))
            .SingleOrDefaultAsync(Ctoken);
    }

    public async Task<bool> DeleteAsync(Guid kursTillfallenId, CancellationToken Ctoken)
    {
        if (kursTillfallenId == Guid.Empty)
        {
            throw new ArgumentException("kursTillfallenId cannot be empty", nameof(kursTillfallenId));
        }

        var entity = await _context.KursTillfalle.Where(e => e.ID == kursTillfallenId).SingleOrDefaultAsync(Ctoken);

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
            .Select(entity => new KurstillfalleDto(
                entity.ID,
                entity.Kurs.Kurskod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.Ort.OrtId,
                entity.Ort
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
            .Where(e => e.ID == kursTillfallenId)
            .Select(entity => new KurstillfalleDto(
                entity.ID,
                entity.Kurs.Kurskod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.Ort.OrtId,
                entity.Ort
                ))
            .SingleOrDefaultAsync(Ctoken);

        return kurstillfalle is null ? null : kurstillfalle;
    }

    public async Task<KurstillfalleDto?> UpdateAsync(Guid kursTillfallenId, UpdateKurstillfalleDto KurstillfalleRequest, CancellationToken Ctoken)
    {
        if (kursTillfallenId == Guid.Empty)
        {
            throw new ArgumentException("kursTillfallenId cannot be empty", nameof(kursTillfallenId));
        }

        var entity = _context.KursTillfalle.SingleOrDefault(e => e.ID == kursTillfallenId)
            ?? throw new KeyNotFoundException($"Kurstillfalle with ID {kursTillfallenId} not found.");

        entity.Startdatum = KurstillfalleRequest.Startdatum;
        entity.Slutdatum = KurstillfalleRequest.Slutdatum;
        entity.MaxSeats = KurstillfalleRequest.Maxseats;
        

        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursTillfalle
            .AsNoTracking()
            .Where(e => e.ID == kursTillfallenId)
            .Select(entity => new KurstillfalleDto(
                entity.ID,
                entity.Kurs.Kurskod,
                entity.Kurs,
                entity.Startdatum,
                entity.Slutdatum,
                entity.MaxSeats,
                entity.Ort.OrtId,
                entity.Ort
                ))
            .SingleOrDefaultAsync(Ctoken);



    }
}
