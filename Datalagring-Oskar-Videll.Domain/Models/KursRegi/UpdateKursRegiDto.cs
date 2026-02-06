

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record UpdateKursRegiDto(

    int KursRegiId,
    int Antagen,
    DateTime RegistrationDate,
    string Status,
    ICollection<DeltagareEntity> DeltagareRegi,
    ICollection<Kurstillfalle_Entity> Kurstillfallen
);
