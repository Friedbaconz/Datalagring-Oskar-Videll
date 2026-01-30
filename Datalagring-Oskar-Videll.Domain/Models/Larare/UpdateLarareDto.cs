
namespace Datalagring_Oskar_Videll.Domain.Models.Larare;

public sealed record UpdateLarareDto(
    string Firstname,
    string? Middlename,
    string Lastname,
    string Kompentens
);
