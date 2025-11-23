using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using JobMatching.Application.Mappers;
using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using System.Net;

namespace JobMatching.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        private static readonly IReadOnlyList<string> ValidRoles = new List<string> { "Candidate", "Company" };

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserResponseDto>> Adicionar(UserRequestDto dto)
        {
            if (!ValidRoles.Contains(dto.Role))
            {
                return OperationResult<UserResponseDto>.Failure(
                    $"Role inválida. Deve ser 'Candidate' ou 'Company'. Recebido: {dto.Role}",
                    (int)HttpStatusCode.BadRequest);
            }

            var entity = dto.ToEntity();
            var newUser = await _userRepository.Adicionar(entity);
            var responseDto = newUser.ToDto();

            return OperationResult<UserResponseDto>.Success(responseDto, (int)HttpStatusCode.Created);
        }

        public async Task<OperationResult<UserResponseDto>> Atualizar(int id, UserRequestDto dto)
        {
            if (!ValidRoles.Contains(dto.Role))
            {
                return OperationResult<UserResponseDto>.Failure(
                    $"Role inválida. Deve ser 'Candidate' ou 'Company'. Recebido: {dto.Role}",
                    (int)HttpStatusCode.BadRequest);
            }

            var entity = dto.ToEntity();
            entity.Id = id;

            var updatedUser = await _userRepository.Atualizar(id, entity);

            if (updatedUser is null)
            {
                return OperationResult<UserResponseDto>.Failure("Usuário não encontrado.", (int)HttpStatusCode.NotFound);
            }

            return OperationResult<UserResponseDto>.Success(updatedUser.ToDto(), (int)HttpStatusCode.OK);
        }

        public async Task<OperationResult<string>> Deletar(int id)
        {
            var isDeleted = await _userRepository.Deletar(id);

            if (!isDeleted)
            {
                return OperationResult<string>.Failure("Usuário não encontrado.", (int)HttpStatusCode.NotFound);
            }

            return OperationResult<string>.Success("Usuário deletado com sucesso.", (int)HttpStatusCode.OK);
        }

        public async Task<OperationResult<UserResponseDto>> ObterPorId(int id)
        {
            var user = await _userRepository.ObterPorId(id);

            if (user is null)
            {
                return OperationResult<UserResponseDto>.Failure("Usuário não encontrado.", (int)HttpStatusCode.NotFound);
            }

            return OperationResult<UserResponseDto>.Success(user.ToDto(), (int)HttpStatusCode.OK);
        }
    }
}
