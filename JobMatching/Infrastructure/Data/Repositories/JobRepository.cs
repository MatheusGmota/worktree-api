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

            if (result is not null)
            {
                if (result is null)
                {
                    result = new JobEntity
                    {
                        Id = entity.Id,
                        Category = entity.Category,
                        Company = entity.Company,
                        Description = entity.Description,
                        Location = entity.Location,
                        Salary = entity.Salary,
                        Title = entity.Title,
                        Type = entity.Type
                    };
                }
                else
                    result = entity;

                _context.Job.Update(entity);
                await _context.SaveChangesAsync();

                return result;
            }
            return null;
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
