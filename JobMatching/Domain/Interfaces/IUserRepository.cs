using JobMatching.Domain.Entities;

namespace JobMatching.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> ObterPorId(int id);
        Task<UserEntity> Adicionar(UserEntity entity);
        Task<UserEntity?> Atualizar(int id, UserEntity entity);
        Task<bool> Deletar(int id);
    }
}
