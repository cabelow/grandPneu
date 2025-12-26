using System.ComponentModel.DataAnnotations;

namespace GrandPneu.Api.DTOs;

public class UserLoginDto
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; } = null!;
}