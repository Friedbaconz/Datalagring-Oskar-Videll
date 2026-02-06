

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.Ort;

public sealed record OrtDto(
    int Ortid,
    string Ortnamn,
    ICollection<Kurstillfalle_Entity> KursTillfalle
);