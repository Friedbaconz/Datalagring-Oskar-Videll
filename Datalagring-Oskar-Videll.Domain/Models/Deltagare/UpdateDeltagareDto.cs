

namespace DatalagringOskarVidell.Domain.Models.Deltagare;

public sealed record UpdateDeltagareDto(
    Guid Id,
    string Firstname,
    string? Middlename,
    string Lastname,
    string Email,
    string? Phonenumber
);