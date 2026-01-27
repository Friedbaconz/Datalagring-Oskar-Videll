namespace Datalagring_Oskar_Videll.Presentation.Api.Models;

public sealed record UpdateDeltagareRequest(string email, string fornamn, string mellannamn, string efternamn, string? telefonnummer)
{
}
