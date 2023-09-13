using System.Security.Cryptography;
using System.Text;

namespace Source.Services;

public static class PasswordHash
{
    public static void CreatePassword(string password,out byte[] passHash, out byte[] passSalt)
    {
        using var hmac = new HMACSHA256();
        passSalt = hmac.Key;
        passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

    }

    public static bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt)
    {
        using var hmac = new HMACSHA256();
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passHash); 

    }
}
