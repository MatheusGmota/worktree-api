using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using JobMatching.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace JobMatching.Infrastructure.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ApplicationEntity?> Adicionar(ApplicationEntity entity)
        {
            _context.Application.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ApplicationEntity?> Atualizar(int id, ApplicationEntity entity)
        {
            var existingEntity = await _context.Application.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEntity is not null)
            {
                // **Observação:** Copiar propriedades manualmente ou usar um mapeador (como AutoMapper) é mais seguro.
                // Aqui estamos apenas atualizando a entidade rastreada com os novos valores.
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                existingEntity.Id = id;

                await _context.SaveChangesAsync();

                return existingEntity;
            }
            return null;
        }

        public async Task<ApplicationEntity?> Deletar(int id)
        {
            var result = await _context.Application.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null) return null;

            _context.Application.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<ApplicationEntity?> ObterPorId(int id)
        {
            // Incluindo navegação para Job e Candidate, se necessário para o DTO de resposta
            return await _context.Application
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
