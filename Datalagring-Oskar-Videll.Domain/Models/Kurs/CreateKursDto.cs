

namespace DatalagringOskarVidell.Domain.Models.Kurs;

public sealed record CreateKursDto(
    string Kurskod,

    string KursNamn,

    string Description
);
