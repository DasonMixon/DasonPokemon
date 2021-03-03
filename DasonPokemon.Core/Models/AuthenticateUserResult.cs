namespace DasonPokemon.Core.Models
{
    public class AuthenticateUserResult
    {
        public bool WasSuccessful { get; set; }
        public string FailureReason { get; set; }
    }
}
