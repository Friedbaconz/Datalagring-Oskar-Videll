

using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Models.Kurs;
using DatalagringOskarVidell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class KursRepository(DeltagareDBContext context) : IKursRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<KursDto> CreateAsync(CreateKursDto KursRequest, CancellationToken Ctoken)
    {
        var entity = new Domain.Entities.Kurs_Entity
        {
            Kurskod = KursRequest.Kurskod,
            Kursnamn = KursRequest.KursNamn,
            Beskrivning = KursRequest.Description
        };

        try
            {
            _context.Kurs.Add(entity);
            await _context.SaveChangesAsync(Ctoken);
            return new KursDto
            (
                entity.Kurskod,
                entity.Kursnamn,
                entity.Beskrivning
            );
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the course.", ex);
        }
    }

    public async Task<bool> DeleteAsync(string Kurskod, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(Kurskod))
        {
            throw new ArgumentException("Kurskod cannot be null or empty", nameof(Kurskod));
        }

        var entity = await _context.Kurs
            .Where(e => e.Kurskod == Kurskod)
            .SingleOrDefaultAsync(Ctoken);

        if (entity == null)
        {
            return false;
        }

        _context.Kurs.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }

    public async Task<IReadOnlyList<KursDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _context.Kurs
            .AsNoTracking()
            .Select(e => new KursDto
            (
                e.Kurskod,
                e.Kursnamn,
                e.Beskrivning
            ))
            .ToListAsync(Ctoken);

        return entities;
    }

    public async Task<KursDto?> GetByKursAsync(string Kurskod, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(Kurskod))
        {
            throw new ArgumentException("Kurskod cannot be null or empty", nameof(Kurskod));
        }

        var entity = await _context.Kurs
            .AsNoTracking()
            .Where(e => e.Kurskod == Kurskod)
            .Select(e => new KursDto
            (
                e.Kurskod,
                e.Kursnamn,
                e.Beskrivning
            ))
            .SingleOrDefaultAsync(Ctoken);

        return entity is null ? null : entity;
    }

    public async Task<KursDto?> UpdateAsync(string Kurskod, UpdateKursDto KursRequest, CancellationToken Ctoken)
    {
        if (string.IsNullOrWhiteSpace(Kurskod))
        {
            throw new ArgumentException("Kurskod cannot be null or empty", nameof(Kurskod));
        }

        var entity = await _context.Kurs
            .Where(e => e.Kurskod == Kurskod)
            .SingleOrDefaultAsync(Ctoken)
            ?? throw new KeyNotFoundException($"Course with Kurskod '{Kurskod}' not found.");

        entity.Kurskod = KursRequest.Kurskod;
        entity.Kursnamn = KursRequest.KursNamn;
        entity.Beskrivning = KursRequest.Description;

        await _context.SaveChangesAsync(Ctoken);
        

        return await _context.Kurs
            .AsNoTracking()
            .Where(e => e.Kurskod == Kurskod)
            .Select(e => new KursDto
            (
                e.Kurskod,
                e.Kursnamn,
                e.Beskrivning
            ))
            .SingleOrDefaultAsync(Ctoken);
    }
}
