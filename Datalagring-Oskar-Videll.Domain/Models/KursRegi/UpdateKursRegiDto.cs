

namespace DatalagringOskarVidell.Domain.Models.KursRegi;

public sealed record UpdateKursRegiDto(
    
    Guid KursRegiId,
    Guid StudentEmail,
    DateTime RegistrationDate,
    string Status
);
