using Asp.Versioning;
using JobMatching.Application.DTOs;
using JobMatching.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobMatching.Controllers.v1
{
    [ApiVersion(1)] // Usando a lib Asp.Versioning.Mvc
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase, ILogger<UserController> logger)
        {
            _logger = logger;
            _logger.LogInformation("UserController v1 inicializado.");
            _userUseCase = userUseCase;
        }

        // --- Obter por ID (GET) ---
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter usuário por ID", Description = "Obtém os detalhes de um usuário específico (Candidato ou Empresa) usando seu ID.")]
        [SwaggerResponse(200, "Usuário obtido com sucesso", typeof(UserResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting user with id: {id}", id);

            var resultado = await _userUseCase.ObterPorId(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        // --- Adicionar (POST) ---
        [HttpPost]
        [SwaggerOperation(Summary = "Registrar novo usuário", Description = "Cria um novo usuário (Candidato ou Empresa).")]
        [SwaggerResponse(201, "Usuário registrado com sucesso", typeof(UserResponseDto))]
        [SwaggerResponse(400, "Role inválida ou dados incompletos")]
        public async Task<IActionResult> Add([FromBody] UserRequestDto dto)
        {
            var resultado = await _userUseCase.Adicionar(dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        // --- Editar (PUT) ---
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar usuário existente", Description = "Edita os detalhes de um usuário existente, incluindo descrição e skills.")]
        [SwaggerResponse(200, "Usuário editado com sucesso", typeof(UserResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Role inválida ou dados incompletos")]
        public async Task<IActionResult> Edit(int id, [FromBody] UserRequestDto dto)
        {
            _logger.LogInformation("Editing user with id: {id}", id);

            var resultado = await _userUseCase.Atualizar(id, dto);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        // --- Deletar (DELETE) ---
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar usuário", Description = "Deleta um usuário existente usando seu ID.")]
        [SwaggerResponse(200, "Usuário deletado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with id: {id}", id);

            var resultado = await _userUseCase.Deletar(id);
            if (!resultado.IsSuccess)
            {
                _logger.LogWarning(resultado.Error);
                return StatusCode(resultado.StatusCode, resultado.Error);
            }
            // Retorna 200 OK com mensagem de sucesso
            return StatusCode(resultado.StatusCode, resultado.Value);
        }
    }
}
