

using Datalagring_Oskar_Videll.Domain.Entities;

namespace Datalagring_Oskar_Videll.Domain.Models.Ort;

public sealed record OrtDto(
    Guid Ortid,
    string Ortnamn
);
