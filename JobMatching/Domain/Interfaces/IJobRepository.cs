using JobMatching.Domain.Entities;

namespace JobMatching.Domain.Interfaces
{
    public interface IJobRepository
    {
        Task<JobEntity?> Adicionar(JobEntity entity);
        Task<JobEntity?> Atualizar(int id, JobEntity entity);
        Task<JobEntity?> Deletar(int id);
        Task<JobEntity?> ObterPorId(int id);
    }
}
