namespace DasonPokemon.Core.Models
{
    public class AuthorizeUserResult
    {
        public bool WasSuccessful { get; set; }
        public string AccessToken { get; set; }
        public string RefreshTokem { get; set; }
    }
}
