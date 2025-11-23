using JobMatching.Application.DTOs;
using JobMatching.Domain.Entities;

namespace JobMatching.Application.Interfaces
{
    public interface IUserUseCase
    {
        Task<OperationResult<UserResponseDto>> ObterPorId(int id);
        Task<OperationResult<UserResponseDto>> Adicionar(UserRequestDto dto);
        Task<OperationResult<UserResponseDto>> Atualizar(int id, UserRequestDto dto);
        Task<OperationResult<string>> Deletar(int id);
    }
}
