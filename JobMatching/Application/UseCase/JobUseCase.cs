using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using JobMatching.Application.Mappers;
using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using System.Net;

namespace JobMatching.Application.UseCase
{
    public class JobUseCase : IJobUseCase
    {

        private readonly IJobRepository _jobRepository;

        public JobUseCase(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<OperationResult<JobResponseDto?>> Adicionar(JobRequestDto dto)
        {
            try
            {
                var result = await _jobRepository.Adicionar(dto.ToEntity());

                if (result is null) return OperationResult<JobResponseDto?>.Failure("Erro ao adicionar");

                return OperationResult<JobResponseDto?>.Success(result.ToDto(), (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return OperationResult<JobResponseDto?>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<JobEntity?>> Deletar(int id)
        {
            try
            {
                var result = await _jobRepository.Deletar(id);

                if (result is null) return OperationResult<JobEntity?>.Failure("Vaga não encontrada");

                return OperationResult<JobEntity?>.Success(result);
            }
            catch (Exception)
            {
                return OperationResult<JobEntity?>.Failure("Erro ao deletar vaga");
            }
        }

        public async Task<OperationResult<JobResponseDto?>> Editar(int id, JobRequestDto dto)
        {
            try
            {
                var result = await _jobRepository.Atualizar(id, dto.ToEntity());

                if (result is null) return OperationResult<JobResponseDto?>.Failure("Vaga não encontrada");

                return OperationResult<JobResponseDto?>.Success(result.ToDto());
            }
            catch (Exception)
            {
                return OperationResult<JobResponseDto?>.Failure("Erro ao editar vaga");
            }
        }

        public async Task<OperationResult<JobResponseDto?>> ObterPorId(int id)
        {
            try
            {
                var result = await _jobRepository.ObterPorId(id);
                if (result is null) return OperationResult<JobResponseDto?>.Failure("Vaga não encontrada");
                return OperationResult<JobResponseDto?>.Success(result.ToDto());
            }
            catch (Exception)
            {
                return OperationResult<JobResponseDto?>.Failure("Erro ao obter vaga");
            }
        }
    }
}
