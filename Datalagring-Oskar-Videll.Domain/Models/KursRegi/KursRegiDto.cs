
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record KursRegiDto(
    int ID,
    Guid RegiID,
    Guid Antagen,
    DateTime RegistrationDate,
    string Status,
    DeltagareEntity DeltagareRegi,
    Kurstillfalle_Entity Kurstillfallen
);
