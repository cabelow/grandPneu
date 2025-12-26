using GrandPneu.Api.DTOs;
using GrandPneu.Infrastructure.Data;
using GrandPneu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace GrandPneu.Api.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Cadastro
    public async Task<UserResponseDto> RegisterAsync(UserRegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email já cadastrado");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var role = dto.Role switch
        {
            1 => UserRole.Admin,
            2 => UserRole.Gestor,
            3 => UserRole.User,
            _ => UserRole.User
        };

        var user = new User(dto.Name, dto.Email, hashedPassword, role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return ToResponseDto(user);
    }

    // Login
    public async Task<UserResponseDto?> LoginAsync(UserLoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return ToResponseDto(user);
    }

    // Listagem
    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        return await _context.Users
            .Select(u => ToResponseDto(u))
            .ToListAsync();
    }

    private static UserResponseDto ToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    public async Task<UserResponseDto> UpdateAsync(Guid userId, UserUpdateDto dto, User actor)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        if (actor.Role != UserRole.Admin)
            throw new UnauthorizedAccessException("Ação permitida apenas para administradores");

        user.ChangeName(dto.Name);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return ToResponseDto(user);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        return user;
    }


}
