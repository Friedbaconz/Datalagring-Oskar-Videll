

namespace DatalagringOskarVidell.Domain.Models.Kurs;

public sealed record UpdateKursDto(
    string KursId,
    string KursNamn,
    string Description
);