

namespace Datalagring_Oskar_Videll.Domain.Models.Kurs;

public sealed record UpdateKursDto(
    string KursId,
    string KursNamn,
    string Description
);