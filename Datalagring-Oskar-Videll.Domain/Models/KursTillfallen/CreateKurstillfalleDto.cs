

namespace Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

using Datalagring_Oskar_Videll.Domain.Entities;
using Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

public sealed record CreateKurstillfalleDto(

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats



);