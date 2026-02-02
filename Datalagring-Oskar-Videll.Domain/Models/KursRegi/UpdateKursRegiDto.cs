

namespace Datalagring_Oskar_Videll.Domain.Models.KursRegi;

public sealed record UpdateKursRegiDto(
    
    Guid KursRegiId,
    string StudentEmail,
    DateTime RegistrationDate,
    string Status
);
