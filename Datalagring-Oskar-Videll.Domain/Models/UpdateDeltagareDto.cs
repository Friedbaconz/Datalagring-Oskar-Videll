namespace Datalagring_Oskar_Videll.Domain.Models;

public sealed record UpdateDeltagareDto(string email, string fornamn, string mellannamn, string efternamn, string? telefonnummer);