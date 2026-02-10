
using DatalagringOskarVidell.Domain.Entities;

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record KursRegiDto(
    Guid RegiID,
    Guid Antagen,
    DateTime RegistrationDate,
    string Status,
    DeltagareEntity DeltagareRegi,
    Kurstillfalle_Entity Kurstillfallen
);
