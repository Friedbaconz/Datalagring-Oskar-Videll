

namespace Datalagring_Oskar_Videll.Domain.Models.Deltagare;

public sealed record UpdateDeltagareDto(
    string Email,
    string Firstname,
    string? Middlename,
    string Lastname,
    string Phonenumber
);