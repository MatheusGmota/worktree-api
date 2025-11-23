using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Mappers
{
    public static class JobMapper
    {
        public static JobResponseDto ToDto(this JobEntity entity) =>
            new JobResponseDto(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.Company,
                entity.Location,
                entity.Salary,
                entity.Type,
                entity.Category
            );
        public static JobEntity ToEntity(this JobRequestDto dto) =>
            new JobEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Company = dto.Company,
                Location = dto.Location,
                Salary = dto.Salary,
                Type = dto.Type,
                Category = dto.Category
            };
    }
}
