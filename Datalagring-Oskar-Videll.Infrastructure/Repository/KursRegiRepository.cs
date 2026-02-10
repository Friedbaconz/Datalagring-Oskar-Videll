
using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.KursRegi;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;
using DatalagringOskarVidell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class KursRegiRepository(DeltagareDBContext context) : IKursRegiRepository
{
    private readonly DeltagareDBContext _context = context;

    public async Task<KursRegiDto?> CreateAsync(CreateKursRegiDto KursRegiRequest, CancellationToken Ctoken)
    {
        var entity = new KursRegi_Entity
        {
            Antagen = KursRegiRequest.Antagen,
            ID = KursRegiRequest.RegiID,
            status = KursRegiRequest.Status,
            RegiDatum = KursRegiRequest.RegistrationDate,
            DeltagareRegi = await _context.Deltagare_Entity.FirstOrDefaultAsync(e => e.ID == KursRegiRequest.Antagen),
            Kurstillfallen = await _context.KursTillfalle.FirstOrDefaultAsync(e => e.ID == KursRegiRequest.RegiID)
        };

        try
        {
            await _context.KursRegi.AddAsync(entity);
            await _context.SaveChangesAsync(Ctoken);
            return await _context.KursRegi
            .AsNoTracking()
            .Where(e => e.ID == entity.ID)
            .Select(entity => new KursRegiDto(
                entity.Kurstillfallen.ID,
                entity.DeltagareRegi.ID,
                entity.RegiDatum,
                entity.status,
                entity.DeltagareRegi,
                entity.Kurstillfallen
                ))
            .SingleOrDefaultAsync(Ctoken);

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

        var entity = await _context.KursRegi.SingleOrDefaultAsync(e => e.ID == Id, Ctoken);

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
                e.ID,
                e.Antagen,
                e.RegiDatum,
                e.status,
                e.DeltagareRegi,
                e.Kurstillfallen
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
            Where(e => e.ID == Id).
            Select(e => new KursRegiDto
            (
                e.ID,
                e.Antagen,
                e.RegiDatum,
                e.status,
                e.DeltagareRegi,
                e.Kurstillfallen
            )).
            SingleOrDefaultAsync(Ctoken);

        return entity is null ? null : entity;
    }

    public async Task<KursRegiDto?> UpdateAsync(Guid Id, UpdateKursRegiDto KursRequest, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty) {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }
        var entity = await _context.KursRegi.SingleOrDefaultAsync(e => e.ID == Id, Ctoken)
            ?? throw new Exception("KursRegi not found");

        entity.status = KursRequest.Status;
        entity.RegiDatum = KursRequest.RegistrationDate;

        await _context.SaveChangesAsync(Ctoken);

        return await _context.KursRegi.
            AsNoTracking().
            Where(e => e.ID == Id).
            Select(e => new KursRegiDto
            (
                e.ID,
                e.Antagen,
                e.RegiDatum,
                e.status,
                e.DeltagareRegi,
                e.Kurstillfallen
            )).
            SingleOrDefaultAsync(Ctoken);



    }
}
