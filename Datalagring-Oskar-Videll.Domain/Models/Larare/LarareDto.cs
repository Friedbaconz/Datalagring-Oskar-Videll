

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Larare;

public sealed record LarareDto(
    string Email,
    string Firstname,
    string? Middlename,
    string Lastname,
    string Kompentens,
    ICollection<KurstillfalleLarare_Entity> Tillfallen
);
