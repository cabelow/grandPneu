using System.Text.RegularExpressions;

namespace GrandPneu.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string name, string email, string passwordHash, UserRole role)
    {
        ValidateName(name);
        ValidateEmail(email);
        ValidatePasswordHash(passwordHash);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public void ChangeName(string newName)
    {
        ValidateName(newName);
        Name = newName;
    }

    public void ChangeEmail(string newEmail, User actor)
    {
        EnsureAdmin(actor);
        ValidateEmail(newEmail);
        Email = newEmail;
    }

    public void ChangeRole(UserRole newRole, User actor)
    {
        EnsureAdmin(actor);
        Role = newRole;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        ValidatePasswordHash(newPasswordHash);
        PasswordHash = newPasswordHash;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio.");
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser vazio.");

        // Regex simples para validação de email
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, emailRegex))
            throw new ArgumentException("Email inválido.");
    }

    private static void ValidatePasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Senha inválida.");
    }

    private static void EnsureAdmin(User actor)
    {
        if (actor.Role != UserRole.Admin)
            throw new InvalidOperationException("Ação permitida apenas para administradores.");
    }
}
