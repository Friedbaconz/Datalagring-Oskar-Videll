namespace Datalagring_Oskar_Videll.Presentation.Api.Models;

public sealed record CreateDeltagareRequest(string fornamn, string mellannamn, string efternamn, string email, string? telefonnummer)
{
}
