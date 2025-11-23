using Asp.Versioning;
using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using JobMatching.Doc.Samples;
using JobMatching.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace JobMatching.Controllers.v2
{
    [ApiVersion(2)] //Usando a lib Asp.Versioning.Mvc
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobUseCase _jobUseCase;
        public JobController(IJobUseCase jobUseCase, ILogger<JobController> logger)
        {
            _jobUseCase = jobUseCase;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter vaga por ID", Description = "Obtém os detalhes de uma vaga específica usando seu ID.")]
        [SwaggerResponse(200, "Vaga obtida com sucesso", typeof(JobResponseDto))]
        [SwaggerResponseExample(200, typeof(JobResponseDtoExample))]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Usando api na versão v2");
            _logger.LogInformation("Getting job with id: {id}", id);


            var resultado = await _jobUseCase.ObterPorId(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);

                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar nova vaga", Description = "Adicione uma nova vaga ao sistema.")]
        [SwaggerResponse(201, "Vaga adicionada com sucesso", typeof(JobResponseDto))]
        [SwaggerResponseExample(201, typeof(JobResponseDtoExample))]
        public async Task<IActionResult> Add([FromBody] JobRequestDto dto)
        {
            _logger.LogInformation("Usando api na versão v2");

            var resultado = await _jobUseCase.Adicionar(dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar vaga existente", Description = "Edite os detalhes de uma vaga existente usando seu ID.")]
        [SwaggerResponse(200, "Vaga editada com sucesso", typeof(JobResponseDto))]
        [SwaggerResponseExample(200, typeof(JobResponseDtoExample))]
        public async Task<IActionResult> Edit(int id, [FromBody] JobRequestDto dto)
        {
            _logger.LogInformation("Usando api na versão v2");
            _logger.LogInformation("Getting job with id: {id}", id);

            var resultado = await _jobUseCase.Editar(id, dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar vaga", Description = "Deleta uma vaga existente usando seu ID.")]
        [SwaggerResponse(200, "Vaga deletada com sucesso", typeof(JobEntity))]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Usando api na versão v2");
            _logger.LogInformation("Getting job with id: {id}", id);

            var resultado = await _jobUseCase.Deletar(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, "Vaga deletada com sucesso");
        }
    }
}
