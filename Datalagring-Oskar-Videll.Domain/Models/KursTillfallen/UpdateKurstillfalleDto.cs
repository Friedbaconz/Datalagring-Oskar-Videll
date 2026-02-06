
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursTillfallen;

public sealed record UpdateKurstillfalleDto(

    Guid KursTillfallenId,

    string KursKod,

    Kurs_Entity Kurs,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    int Ortid,

    Ort_Entity Ort
);
