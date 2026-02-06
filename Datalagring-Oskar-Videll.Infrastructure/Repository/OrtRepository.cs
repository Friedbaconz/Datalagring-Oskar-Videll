

using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Models.Ort;
using DatalagringOskarVidell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class OrtRepository(DeltagareDBContext context) : IOrtRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<OrtDto> CreateAsync(CreateOrtDto OrtRequest, CancellationToken Ctoken)
    {
        var entity = new Domain.Entities.Ort_Entity
        {
            OrtNamn = OrtRequest.Ortnamn
        };

        try 
        {
            _context.Ort.Add(entity);
            await _context.SaveChangesAsync(Ctoken);
            return new OrtDto
            (
                entity.OrtId,
                entity.OrtNamn,
                entity.Kurstillfallen
            );
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the Ort.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int ortId, CancellationToken Ctoken)
    {
        if (ortId == 0)
        {
            throw new ArgumentException("OrtId cannot be empty", nameof(ortId));
        }

        var entity = await _context.Ort
            .Where(e => e.OrtId == ortId)
            .SingleOrDefaultAsync(Ctoken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Ort with id {ortId} not found");
        }

        _context.Ort.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }

    public async Task<IReadOnlyList<OrtDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _context.Ort.
            AsNoTracking().
            Select(e => new OrtDto
            (
                e.OrtId,
                e.OrtNamn,
                e.Kurstillfallen
            ))
            .ToListAsync(Ctoken);

        return entities;
    }

    public async Task<OrtDto?> GetByIdAsync(int ortId, CancellationToken Ctoken)
    {
        if (ortId == 0)
        {
            throw new ArgumentException("OrtId cannot be empty", nameof(ortId));
        }

        var Ort = await _context.Ort
            .AsNoTracking()
            .Where(e => e.OrtId == ortId)
            .Select(e => new OrtDto
            (
                e.OrtId,
                e.OrtNamn,
                e.Kurstillfallen
            ))
            .SingleOrDefaultAsync(Ctoken);

        return Ort is null ? null : Ort;

    }

    public async Task<OrtDto?> UpdateAsync(int ortId, UpdateOrtDto OrtRequest, CancellationToken Ctoken)
    {
        if (ortId == 0)
        {
            throw new ArgumentException("OrtId cannot be empty", nameof(ortId));
        }

        var entity = await _context.Ort
            .Where(e => e.OrtId == ortId)
            .SingleOrDefaultAsync(Ctoken)
            ?? throw new KeyNotFoundException($"Ort with Id {ortId} not found.");

        entity.OrtId = ortId;
        entity.OrtNamn = OrtRequest.Ortnamn;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.Ort
            .AsNoTracking()
            .Where(e => e.OrtId == ortId)
            .Select(e => new OrtDto
            (
                e.OrtId,
                e.OrtNamn,
                e.Kurstillfallen
            ))
            .SingleOrDefaultAsync(Ctoken);
    }
}
