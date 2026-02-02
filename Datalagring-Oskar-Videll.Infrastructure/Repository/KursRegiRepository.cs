
using Datalagring_Oskar_Videll.Application.Contracts;
using Datalagring_Oskar_Videll.Domain.Entities;
using Datalagring_Oskar_Videll.Domain.Models.KursRegi;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Repository;

public class KursRegiRepository(DeltagareDBContext context) : IKursRegiRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<KursRegiDto> CreateAsync(CreateKursRegiDto KursRegiRequest, CancellationToken Ctoken)
    {
        var entity = new KursRegi_Entity
        {
            status = KursRegiRequest.Status,
            RegiDatum = KursRegiRequest.RegistrationDate
        };

        try
        {
            _context.KursRegi.Add(entity);
            await _context.SaveChangesAsync(Ctoken);
            return new KursRegiDto
            (

                entity.KursRegiId,
                entity.DeltagareEmail,
                entity.RegiDatum,
                entity.status
            );

        }
        catch (Exception ex)
        {
            throw new Exception("Could not create KursRegi", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }

        var entity = await _context.KursRegi.SingleOrDefaultAsync(e => e.KursRegiId == Id, Ctoken);

        if (entity == null)
        {
            return false;
        }

        _context.KursRegi.Remove(entity);
        await _context.SaveChangesAsync(Ctoken);
        return true;
    }

    public async Task<IReadOnlyList<KursRegiDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _context.KursRegi.
            AsNoTracking().
            Select(e => new KursRegiDto
            (
                e.KursRegiId,
                e.DeltagareEmail,
                e.RegiDatum,
                e.status
            )).
            ToListAsync(Ctoken);

        return entities;
    }

    public async Task<KursRegiDto?> GetByIDAsync(Guid Id, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }

        var entity = await _context.KursRegi.
            AsNoTracking().
            Where(e => e.KursRegiId == Id).
            Select(e => new KursRegiDto
            (
                e.KursRegiId,
                e.DeltagareEmail,
                e.RegiDatum,
                e.status
            )).
            SingleOrDefaultAsync(Ctoken);

        return entity is null ? null : entity;
    }

    public async Task<KursRegiDto?> UpdateAsync(Guid Id, UpdateKursRegiDto KursRequest, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty) {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }
        var entity = await _context.KursRegi.SingleOrDefaultAsync(e => e.KursRegiId == Id, Ctoken)
            ?? throw new Exception("KursRegi not found");

        entity.status = KursRequest.Status;
        entity.RegiDatum = KursRequest.RegistrationDate;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursRegi.
            AsNoTracking().
            Where(e => e.KursRegiId == Id).
            Select(e => new KursRegiDto
            (
                e.KursRegiId,
                e.DeltagareEmail,
                e.RegiDatum,
                e.status
            )).
            SingleOrDefaultAsync(Ctoken);



    }
}
