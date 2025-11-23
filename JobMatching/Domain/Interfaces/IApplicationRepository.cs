using JobMatching.Domain.Entities;

namespace JobMatching.Domain.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ApplicationEntity?> Adicionar(ApplicationEntity entity);
        Task<ApplicationEntity?> Atualizar(int id, ApplicationEntity entity);
        Task<ApplicationEntity?> Deletar(int id);
        Task<ApplicationEntity?> ObterPorId(int id);
    }
}
