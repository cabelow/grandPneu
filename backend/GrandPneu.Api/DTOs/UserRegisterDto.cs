using System.ComponentModel.DataAnnotations;

namespace GrandPneu.Api.DTOs;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres")]
    [MaxLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido, deve seguir o formato nome@dominio.com")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Role é obrigatória")]
    [Range(1, 3, ErrorMessage = "Role deve ser 1 (Admin), 2 (Gestor) ou 3 (User)")]
    public int Role { get; set; }
}
