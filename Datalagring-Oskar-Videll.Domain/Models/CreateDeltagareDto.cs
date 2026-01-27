

namespace Datalagring_Oskar_Videll.Domain.Models;

public sealed record CreateDeltagareDto(string fornamn, string mellannamn, string efternamn, string email, string? telefonnummer);
