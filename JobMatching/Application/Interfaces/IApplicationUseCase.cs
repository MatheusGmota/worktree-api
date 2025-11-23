using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Interfaces
{
    public interface IApplicationUseCase
    {
        Task<OperationResult<ApplicationResponseDto?>> Adicionar(ApplicationRequestDto dto);
        Task<OperationResult<ApplicationResponseDto?>> Editar(int id, ApplicationRequestDto dto);
        Task<OperationResult<ApplicationEntity?>> Deletar(int id);
        Task<OperationResult<ApplicationResponseDto?>> ObterPorId(int id);
    }
}