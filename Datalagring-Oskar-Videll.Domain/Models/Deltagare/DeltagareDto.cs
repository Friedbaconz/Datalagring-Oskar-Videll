

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Deltagare;

public sealed record DeltagareDto(
    Guid Id,
    string Firstname,
    string? Middlename,
    string Lastname,
    string Email,
    string? Phonenumber,
    ICollection<Kurstillfalle_Entity> Antagnakurser
);

