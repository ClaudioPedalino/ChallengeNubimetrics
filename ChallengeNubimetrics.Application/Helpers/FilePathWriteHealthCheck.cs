using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Helpers
{
    public class FilePathWriteHealthCheck : IHealthCheck
    {
        private readonly string _filePath;
        private IReadOnlyDictionary<string, object> _HealthCheckData;

        public FilePathWriteHealthCheck(string filePath)
        {
            _filePath = filePath;
            _HealthCheckData = new Dictionary<string, object>
            {
                {"filePath", _filePath }
            };
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var testFile = Directory.GetCurrentDirectory() + $"{_filePath}\\test.txt";
                var fs = File.Create(testFile);
                fs.Close();
                File.Delete(testFile);

                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                return context.Registration.FailureStatus switch
                {
                    HealthStatus.Degraded => Task.FromResult(HealthCheckResult.Degraded($"Issues writing to file path", ex, _HealthCheckData)),
                    _ => Task.FromResult(HealthCheckResult.Unhealthy($"Issues writing to file path", ex, _HealthCheckData)),
                };
            }
        }
    }
}
