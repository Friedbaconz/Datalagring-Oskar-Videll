

namespace Datalagring_Oskar_Videll.Domain.Models.Deltagare;

public sealed record UpdateDeltagareDto(
    string Firstname,
    string? Middlename,
    string Lastname,
    string? Phonenumber
);