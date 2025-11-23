using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using JobMatching.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace dashmottu.API.Infrastructure.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationContext _context;

        public JobRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<JobEntity?> Adicionar(JobEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<JobEntity?> Atualizar(int id, JobEntity entity)
        {
            var result = await _context.Job.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return null;
            }

            result.Category = entity.Category;
            result.Company = entity.Company;
            result.Description = entity.Description;
            result.Location = entity.Location;
            result.Salary = entity.Salary;
            result.Title = entity.Title;
            result.Type = entity.Type;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<JobEntity?> Deletar(int id)
        {
            var result = await _context.Job.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null) return null;

            _context.Job.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<JobEntity?> ObterPorId(int id)
        {
            return await _context.Job.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
