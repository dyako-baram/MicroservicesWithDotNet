using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HelloDotnet5
{
    public class ExternalEndpoinHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _setting;

        public ExternalEndpoinHealthCheck(IOptions<ServiceSettings> setting)
        {
            _setting = setting.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping =new();
            var reply=await  ping.SendPingAsync(_setting.OpenWeatherHost);
            if(reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}