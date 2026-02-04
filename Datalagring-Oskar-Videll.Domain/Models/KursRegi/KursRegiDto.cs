
namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record KursRegiDto(
    Guid KursRegiId,
    Guid StudentEmail,
    DateTime RegistrationDate,
    string Status
);
