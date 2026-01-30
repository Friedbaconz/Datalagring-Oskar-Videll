
using Datalagring_Oskar_Videll.Domain.Entities;

namespace Datalagring_Oskar_Videll.Domain.Models.KursTillfallen;

public sealed record UpdateKurstillfalleDto(

    Guid KursTillfallenId,

    string KursKod,

    Kurs_Entity kurs,

    DateTime Startdatum,

    DateTime Slutdatum,

    int Maxseats,

    string LarareEmail,

    int Ortid,

    Ort_Entity Ort,

    ICollection<KursRegi_Entity> KursrgisteringsId,

    ICollection<KurstillfalleLarare_Entity> LarareTillfallenId

);
