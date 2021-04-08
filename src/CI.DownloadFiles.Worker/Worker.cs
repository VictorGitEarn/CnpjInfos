using CI.DownloadFiles.Worker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CI.DownloadFiles.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDownloadFileService _service;

        public Worker(ILogger<Worker> logger, IDownloadFileService service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    _service.Execute();

                    //12h em 12h - 43200000
                    await Task.Delay(43200000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Worker error: {ex}", ex);
                }
            }
        }
    }
}
