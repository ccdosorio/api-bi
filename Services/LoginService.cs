using api_bi.DTOs;
using api_bi.Models;
using Microsoft.EntityFrameworkCore;

namespace api_bi.Services;

public class LoginService : ILoginService
{
    BiContext _context;

    public LoginService(BiContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(UserDto user)
    {
        return await _context.Users.
        SingleOrDefaultAsync(u => u.email == user.email && u.password == user.password);
    }
}

public interface ILoginService
{
    Task<User?> GetUser(UserDto user);
}