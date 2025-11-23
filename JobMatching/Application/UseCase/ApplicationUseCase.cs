using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using JobMatching.Application.Mappers;
using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using System.Net;

namespace JobMatching.Application.UseCase
{
    public class ApplicationUseCase : IApplicationUseCase
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationUseCase(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<OperationResult<ApplicationResponseDto?>> Adicionar(ApplicationRequestDto dto)
        {
            try
            {
                var result = await _applicationRepository.Adicionar(dto.ToEntity());

                if (result is null) return OperationResult<ApplicationResponseDto?>.Failure("Erro ao adicionar aplicação");

                return OperationResult<ApplicationResponseDto?>.Success(result.ToDto(), (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return OperationResult<ApplicationResponseDto?>.Failure("Erro interno ao adicionar aplicação.");
            }
        }

        public async Task<OperationResult<ApplicationEntity?>> Deletar(int id)
        {
            try
            {
                var result = await _applicationRepository.Deletar(id);

                if (result is null) return OperationResult<ApplicationEntity?>.Failure("Aplicação não encontrada");

                return OperationResult<ApplicationEntity?>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<ApplicationEntity?>.Failure("Erro ao deletar aplicação");
            }
        }

        public async Task<OperationResult<ApplicationResponseDto?>> Editar(int id, ApplicationRequestDto dto)
        {
            try
            {
                var result = await _applicationRepository.Atualizar(id, dto.ToEntity());

                if (result is null) return OperationResult<ApplicationResponseDto?>.Failure("Aplicação não encontrada");

                return OperationResult<ApplicationResponseDto?>.Success(result.ToDto());
            }
            catch (Exception)
            {
                return OperationResult<ApplicationResponseDto?>.Failure("Erro ao editar aplicação");
            }
        }

        public async Task<OperationResult<ApplicationResponseDto?>> ObterPorId(int id)
        {
            try
            {
                var result = await _applicationRepository.ObterPorId(id);
                if (result is null) return OperationResult<ApplicationResponseDto?>.Failure("Aplicação não encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<ApplicationResponseDto?>.Success(result.ToDto());
            }
            catch (Exception)
            {
                return OperationResult<ApplicationResponseDto?>.Failure("Erro ao obter aplicação");
            }
        }
    }
}
