using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Mappers
{
    public static class ApplicationMapper
    {
        public static ApplicationEntity ToEntity(this ApplicationRequestDto dto)
        {
            return new ApplicationEntity
            {
                JobId = dto.JobId,
                CandidateId = dto.CandidateId,
                CoverLetter = dto.coverLetter,
                Status = "PENDING"
            };
        }

        public static ApplicationResponseDto ToDto(this ApplicationEntity entity)
        {
            if (entity.Job == null || entity.Candidate == null)
            {
                throw new InvalidOperationException("As entidades Job e Candidate devem ser carregadas para criar o Response DTO.");
            }

            return new ApplicationResponseDto(
                Id: entity.Id,
                coverLetter: entity.CoverLetter,
                Job: entity.Job.ToDto(),
                Candidate: entity.Candidate.ToDto()
                Status: entity.Status
            );
        }
    }
}
