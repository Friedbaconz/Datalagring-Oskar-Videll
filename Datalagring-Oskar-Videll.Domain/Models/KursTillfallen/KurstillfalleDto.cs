

using Datalagring_Oskar_Videll.Domain.Entities;

namespace Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

public sealed record KurstillfalleDto(

    Guid KursTillfallenId,

    string KursKod,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    string LarareEmail,

    Guid Ortid
 );

