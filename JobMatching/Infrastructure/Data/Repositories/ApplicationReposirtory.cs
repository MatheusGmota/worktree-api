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
            var existingApplication = await _context.Application
                .FirstOrDefaultAsync(a =>
                    a.JobId == entity.JobId &&
                    a.CandidateId == entity.CandidateId);

            if (existingApplication != null)
            {
                return null;
            }

            _context.Application.Add(entity);
            await _context.SaveChangesAsync();

            var result = await _context.Application
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .FirstOrDefaultAsync(a => a.Id == entity.Id);

            return result;
        }

        public async Task<ApplicationEntity?> Atualizar(int id, ApplicationEntity entity)
        {
            var existingEntity = await _context.Application.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEntity is not null)
            {
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
            return await _context.Application
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
