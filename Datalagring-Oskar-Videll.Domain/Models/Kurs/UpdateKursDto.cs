

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Kurs;

public sealed record UpdateKursDto(
    string Kurskod,
    string KursNamn,
    string Description,
    ICollection<Kurstillfalle_Entity> Kurstillfalle
);