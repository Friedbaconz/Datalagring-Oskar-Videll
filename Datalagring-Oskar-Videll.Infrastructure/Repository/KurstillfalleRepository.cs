
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
        var Start = DateTime.SpecifyKind(KurstillfalleRequest.Startdatum, DateTimeKind.Utc);
        var End = DateTime.SpecifyKind(KurstillfalleRequest.Slutdatum, DateTimeKind.Utc);

        if (Start < DateTime.Now.Date) 
        {
            throw new InvalidOperationException("Start can't be earlier than current date");
        }

        if (Start > End)
        {
            throw new InvalidOperationException("Start can't be later than end time");
        }

        var entity = new Kurstillfalle_Entity
        {

            ID = Guid.NewGuid(),
            KursKodID = KurstillfalleRequest.Kurskod,
            Startdatum = Start,
            Slutdatum = End,
            MaxSeats = KurstillfalleRequest.Maxseats,
            Kurs = _context.Kurs.FirstOrDefault(e => e.Kurskod == KurstillfalleRequest.Kurskod),
            Ortid = KurstillfalleRequest.OrtId,
            Ort = _context.Ort.FirstOrDefault(e => e.OrtId == KurstillfalleRequest.OrtId)
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
                entity.Ort,
                entity.KursTillfallenLarare,
                entity.KursRegi
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
                entity.Ort,
                entity.KursTillfallenLarare,
                entity.KursRegi
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
                entity.Ort,
                entity.KursTillfallenLarare,
                entity.KursRegi
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

        var Start = DateTime.SpecifyKind(KurstillfalleRequest.Startdatum, DateTimeKind.Utc);
        var End = DateTime.SpecifyKind(KurstillfalleRequest.Slutdatum, DateTimeKind.Utc);

        if (Start < DateTime.Now.Date)
        {
            throw new InvalidOperationException("Start can't be earlier than current date");
        }

        if (Start > End)
        {
            throw new InvalidOperationException("Start can't be later than end time");
        }

        entity.Startdatum = Start;
        entity.Slutdatum = End;
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
                entity.Ort,
                entity.KursTillfallenLarare,
                entity.KursRegi
                ))
            .SingleOrDefaultAsync(Ctoken);



    }
}
