
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Ort;

public sealed record UpdateOrtDto(
    Guid Ortid,
    string Ortnamn
);
