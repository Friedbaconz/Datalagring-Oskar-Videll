using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.KursRegi;
using DatalagringOskarVidell.Domain.Models.KursRegi.LarareRegi;
using DatalagringOskarVidell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatalagringOskarVidell.Infrastructure.Repository;

public class LarareRegiRepository(DeltagareDBContext dbContext) : ILarareRegiRepository
{
    private readonly DeltagareDBContext _Context = dbContext;

    public async Task<LarareRegiDto?> CreateAsync(CreateLarareRegiDto LarareRegiRequest, CancellationToken Ctoken)
    {
        var entity = new KurstillfalleLarare_Entity
            {
            Larare = LarareRegiRequest.LarareEmail,
            ID = LarareRegiRequest.LarareRegiId,
            LarareRegi = await _Context.Larare.FirstOrDefaultAsync(e => e.Email == LarareRegiRequest.LarareEmail),
            Kurstillfallen = await _Context.KursTillfalle.FirstOrDefaultAsync(e => e.ID == LarareRegiRequest.LarareRegiId)
            };

        try
        {
            await _Context.Larare_Kurstillfalle.AddAsync(entity);
            await _Context.SaveChangesAsync(Ctoken);

            return await _Context.Larare_Kurstillfalle
            .AsNoTracking()
            .Where(e => e.ID == entity.ID)
            .Select(entity => new LarareRegiDto(
                entity.IDUQ,
                entity.Kurstillfallen.ID,
                entity.LarareRegi.Email,
                entity.LarareRegi,
                entity.Kurstillfallen

                ))
            .SingleOrDefaultAsync(Ctoken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating LarareRegi: {ex.Message}");
            throw;
        }


    }

    public async Task<bool> DeleteAsync(Guid Id, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }

        var entity = await _Context.Larare_Kurstillfalle.SingleOrDefaultAsync(e => e.ID == Id, Ctoken);

        if (entity == null)
        {
            return false;
        }

        _Context.Larare_Kurstillfalle.Remove(entity);
        await _Context.SaveChangesAsync(Ctoken);

        return true;
    }

    public async Task<IReadOnlyList<LarareRegiDto>> GetAllAsync(CancellationToken Ctoken)
    {
        var entities = await _Context.Larare_Kurstillfalle
            .AsNoTracking()
            .Select(entity => new LarareRegiDto
            (
                entity.IDUQ,
                entity.ID,
                entity.Larare,
                entity.LarareRegi,
                entity.Kurstillfallen
            ))
            .ToListAsync(Ctoken);

        return entities;
    }

    public async Task<LarareRegiDto?> GetByIdAsync(Guid Id, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }

        var entity = await _Context.Larare_Kurstillfalle
            .AsNoTracking()
            .Select(e => new LarareRegiDto
            (
                e.IDUQ,
                e.ID,
                e.Larare,
                e.LarareRegi,
                e.Kurstillfallen
            ))
            .SingleOrDefaultAsync(e => e.LarareRegiId == Id, Ctoken);

        return entity is null ? null : entity;
    }

    public async Task<LarareRegiDto?> UpdateAsync(Guid Id, UpdateLarareRegiDto KurstillfalleRequest, CancellationToken Ctoken)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }

        var entity = await _Context.Larare_Kurstillfalle.SingleOrDefaultAsync(e => e.ID == Id, Ctoken)
            ?? throw new KeyNotFoundException($"LarareRegi with Id {Id} not found.");

        entity.Larare = KurstillfalleRequest.LarareEmail;
        entity.ID = KurstillfalleRequest.LarareRegiId;

        await _Context.SaveChangesAsync(Ctoken);

        return await _Context.Larare_Kurstillfalle
            .AsNoTracking()
            .Select(e => new LarareRegiDto
            (
                e.IDUQ,
                e.ID,
                e.Larare,
                e.LarareRegi,
                e.Kurstillfallen
            ))
            .SingleOrDefaultAsync(e => e.LarareRegiId == Id, Ctoken);

    }
}
