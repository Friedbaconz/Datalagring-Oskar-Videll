

namespace DatalagringOskarVidell.Domain.Models.Kurs;

public sealed record UpdateKursDto(
    string Kurskod,
    string KursNamn,
    string Description
);