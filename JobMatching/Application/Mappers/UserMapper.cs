using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Mappers
{
    public static class UserMappers
    {
        public static UserEntity ToEntity(this UserRequestDto dto)
        {
            var userEntity = new UserEntity
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Description = dto.Description,
                Role = dto.Role
            };

            if (dto.Skills != null)
            {
                userEntity.Skills = dto.Skills
                    .Select(skillName => new UserSkill
                    {
                        SkillName = skillName,
                    })
                    .ToList();
            }

            return userEntity;
        }

        public static UserResponseDto ToDto(this UserEntity entity)
        {
            return new UserResponseDto(
                Id: entity.Id,
                Name: entity.Name,
                Email: entity.Email,
                Description: entity.Description ?? string.Empty,
                Skills: entity.Skills ?? new List<UserSkill>()
            );
        }

        public static UserResponseDto ToSimpleDto(this UserEntity entity)
        {
            return new UserResponseDto(
                Id: entity.Id,
                Name: entity.Name,
                Email: entity.Email,
                Description: entity.Description ?? string.Empty,
                Skills: entity.Skills ?? new List<UserSkill>()
            );
        }
    }
}
