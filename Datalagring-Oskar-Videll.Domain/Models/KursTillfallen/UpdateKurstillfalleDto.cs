
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursTillfallen;

public sealed record UpdateKurstillfalleDto(

    Guid KursTillfallenId,

    string KursKod,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    string LarareEmail,

    Guid Ortid

);
