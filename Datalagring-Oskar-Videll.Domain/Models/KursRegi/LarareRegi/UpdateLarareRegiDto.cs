
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi.LarareRegi;

public sealed record UpdateLarareRegiDto(
    int id,
    Guid LarareRegiId,
    string LarareEmail,
    Larare_Entity LarareRegi,
    Kurstillfalle_Entity Kurstillfallen
);
