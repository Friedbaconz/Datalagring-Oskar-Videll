

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
                entity.OrtNamn
            );
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the Ort.", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid ortId, CancellationToken Ctoken)
    {
        if (ortId == Guid.Empty)
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
                e.OrtNamn
            ))
            .ToListAsync(Ctoken);

        return entities;
    }

    public async Task<OrtDto?> GetByIdAsync(Guid ortId, CancellationToken Ctoken)
    {
        if (ortId == Guid.Empty)
        {
            throw new ArgumentException("OrtId cannot be empty", nameof(ortId));
        }

        var Ort = await _context.Ort
            .AsNoTracking()
            .Where(e => e.OrtId == ortId)
            .Select(e => new OrtDto
            (
                e.OrtId,
                e.OrtNamn
            ))
            .SingleOrDefaultAsync(Ctoken);

        return Ort is null ? null : Ort;

    }

    public async Task<OrtDto?> UpdateAsync(Guid ortId, UpdateOrtDto OrtRequest, CancellationToken Ctoken)
    {
        if (ortId == Guid.Empty)
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
                e.OrtNamn
            ))
            .SingleOrDefaultAsync(Ctoken);
    }
}
