

using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record UpdateKursRegiDto(
    int ID,
    Guid KursRegiId,
    Guid Antagen,
    DateTime RegistrationDate,
    string Status,
    DeltagareEntity DeltagareRegi,
    Kurstillfalle_Entity Kurstillfallen
);
