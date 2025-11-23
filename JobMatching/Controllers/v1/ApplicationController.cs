using Asp.Versioning;
using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobMatching.Controllers.v1
{
    [ApiVersion(1)] //Usando a lib Asp.Versioning.Mvc
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationUseCase _applicationUseCase;

        public ApplicationController(IApplicationUseCase applicationUseCase, ILogger<ApplicationController> logger)
        {
            _applicationUseCase = applicationUseCase;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter aplicação por ID", Description = "Obtém os detalhes de uma aplicação específica usando seu ID.")]
        [SwaggerResponse(200, "Aplicação obtida com sucesso", typeof(ApplicationResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Usando api na versão v1");
            _logger.LogInformation("Getting application with id: {id}", id);

            var resultado = await _applicationUseCase.ObterPorId(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        // --- Adicionar (POST) ---
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar nova aplicação", Description = "Adiciona uma nova aplicação a uma vaga.")]
        [SwaggerResponse(201, "Aplicação adicionada com sucesso", typeof(ApplicationResponseDto))]
        public async Task<IActionResult> Add([FromBody] ApplicationRequestDto dto)
        {
            _logger.LogInformation("Usando api na versão v1");
            var resultado = await _applicationUseCase.Adicionar(dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar aplicação existente", Description = "Edita os detalhes de uma aplicação existente (e.g., status) usando seu ID.")]
        [SwaggerResponse(200, "Aplicação editada com sucesso", typeof(ApplicationResponseDto))]
        public async Task<IActionResult> Edit(int id, [FromBody] ApplicationRequestDto dto)
        {
            _logger.LogInformation("Usando api na versão v1");
            _logger.LogInformation("Editing application with id: {id}", id);

            var resultado = await _applicationUseCase.Editar(id, dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar aplicação", Description = "Deleta uma aplicação existente usando seu ID.")]
        [SwaggerResponse(200, "Aplicação deletada com sucesso")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Usando api na versão v1");
            _logger.LogInformation("Deleting application with id: {id}", id);

            var resultado = await _applicationUseCase.Deletar(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            // Retorna 204 No Content ou 200 OK com mensagem de sucesso
            return StatusCode(resultado.StatusCode, "Aplicação deletada com sucesso");
        }
    }
}
