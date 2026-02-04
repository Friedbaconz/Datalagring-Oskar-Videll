

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Ort;

public sealed record OrtDto(
    Guid Ortid,
    string Ortnamn
);