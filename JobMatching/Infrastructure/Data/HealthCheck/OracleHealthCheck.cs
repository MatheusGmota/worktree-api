using JobMatching.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Revisao.Infra.Data.HealthCheck
{
    public class OracleHealthCheck : IHealthCheck
    {
        private readonly ApplicationContext _context;

        public OracleHealthCheck(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Job.AsNoTracking().Take(1).CountAsync(cancellationToken);

                return HealthCheckResult.Healthy("Banco de dados esta online");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Banco de dados esta offline", ex);
            }
        }

    }
}
