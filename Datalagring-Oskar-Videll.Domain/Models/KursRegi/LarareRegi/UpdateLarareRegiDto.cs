
namespace Datalagring_Oskar_Videll.Domain.Models.KursRegi.LarareRegi;

public sealed record UpdateLarareRegiDto(
    Guid LarareRegiId,
    string LarareEmail
);
