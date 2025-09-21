namespace Tabpol.Models.Responses
{
    public class TokenResponseDto
    {
        public required string AccesToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
