using System;
using System.Collections.Generic;
using System.Text;

namespace Datalagring_Oskar_Videll.Domain.Models.KursTillfallen.KurstillfalleLarare
{
    public sealed record CreateKurstillFalleLarareDto(
        Guid KursTillfallenId,
        string LarareEmail
    );
}
