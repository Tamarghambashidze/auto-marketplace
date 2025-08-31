namespace FinalProject.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(string email);
    }
}