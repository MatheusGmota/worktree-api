using JobMatching.Domain.Entities;

namespace JobMatching.Application.DTOs
{
    public record UserResponseDto(
        int Id,
        string Name,
        string Email,
        string Description,
        ICollection<UserSkill> Skills
    );
    public record UserRequestDto(
        string Name,
        string Email,
        string Password,
        string Role,
        string? Description,
        ICollection<string> Skills
    );
}
