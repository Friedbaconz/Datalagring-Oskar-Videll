
namespace DatalagringOskarVidell.Domain.Models.Larare;

public sealed record UpdateLarareDto(
    string Email,
    string Firstname,
    string? Middlename,
    string Lastname,
    string Kompentens
);
