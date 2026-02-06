

namespace DatalagringOskarVidell.Domain.Models.KursTillfallen;

using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;

public sealed record CreateKurstillfalleDto(

    string Kurskod,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    int OrtId
);