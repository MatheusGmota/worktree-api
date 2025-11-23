using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Interfaces
{
    public interface IJobUseCase
    {
        Task<OperationResult<JobResponseDto?>> ObterPorId(int id);
        Task<OperationResult<JobResponseDto?>> Adicionar(JobRequestDto dto);
        Task<OperationResult<JobResponseDto?>> Editar(int id, JobRequestDto dto);
        Task<OperationResult<JobEntity?>> Deletar(int id);
    }
}