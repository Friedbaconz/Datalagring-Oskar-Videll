
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi.LarareRegi;

public sealed record UpdateLarareRegiDto(
    int LarareRegiId,
    string LarareEmail,
    ICollection<Larare_Entity> LarareRegi,
    ICollection<Kurstillfalle_Entity> Kurstillfallen
);
