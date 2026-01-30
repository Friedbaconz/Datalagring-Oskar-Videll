

namespace Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

using Datalagring_Oskar_Videll.Domain.Entities;
using Datalagring_Oskar_Videll.Domain.Models.KursTillfallen.KurstillfalleLarare;

public sealed record CreateKurstillfalleDto(

    string KursKod,

    Kurs_Entity kurs,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    int Ortid,

    Ort_Entity Ort,

    ICollection<KursRegi_Entity> KursrgisteringsId,
    ICollection<KurstillfalleLarare_Entity> LarareTillfallenId

);