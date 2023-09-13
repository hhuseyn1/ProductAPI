using Source.Data;
using Source.Models;
using Source.Models.DTOs;

namespace Source.Services;

public class LoginRegisterService : ILoginRegisterService
{
    private readonly AppDbContext _context;

    public LoginRegisterService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Login(UserDTO request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username) ??
            throw new Exception("Username or password is wrong");
        if (PasswordHash.VerifyPasswordHash(request.Password, user.PassHash, user.PashSalt))
            throw new Exception("Username or password is wrong");
        string token = JWTAuthService.CreateToken(user);
        return token;
    }

    public async Task<bool> Register(UserDTO request)
    {
        if (_context.Users.Any(u => u.Username == request.Username)) throw new Exception("Username is already taken");
         
        PasswordHash.CreatePassword(request.Password, out byte[] passHash, out byte[] passSalt);
        User user = new()
        {
            Username = request.Username,
            PassHash = passHash,
            PashSalt = passSalt
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
