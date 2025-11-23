using JobMatching.Domain.Entities;
using JobMatching.Domain.Interfaces;
using JobMatching.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace JobMatching.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Adicionar(UserEntity entity)
        {
            await _context.User.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntity?> Atualizar(int id, UserEntity entity)
        {
            var result = await _context.User
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return null;
            }

            result.Name = entity.Name;
            result.Email = entity.Email;
            result.Password = entity.Password;
            result.Role = entity.Role;
            result.Description = entity.Description;

            if (result.Skills != null)
            {
                _context.UserSkill.RemoveRange(result.Skills);
            }
            result.Skills = entity.Skills;

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> Deletar(int id)
        {
            var result = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                return false;
            }

            _context.User.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserEntity?> ObterPorId(int id)
        {
            return await _context.User
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
