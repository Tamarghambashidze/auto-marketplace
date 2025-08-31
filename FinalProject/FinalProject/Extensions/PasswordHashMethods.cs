 namespace FinalProject.Extensions
{
  public static class PasswordHashMethods
  {
    public static string GetPasswordHash(this string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool VerifyPassword(this string password, string hashedPassword)
    {
      return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
  }
}
