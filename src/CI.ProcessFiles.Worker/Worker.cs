using CI.ProcessFiles.Domain.Services.ExtractFiles;
using CI.ProcessFiles.Domain.Services.ReadFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CI.ProcessFiles.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IExtractFileService _extractFileService;
        private readonly IReadFileService _readFileService;

        public Worker(ILogger<Worker> logger, IExtractFileService extractFileService, IReadFileService readFileService)
        {
            _logger = logger;
            _extractFileService = extractFileService;
            _readFileService = readFileService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    _extractFileService.ExtractFiles();

                    _readFileService.ReadFiles();

                    //24h em 24h - 86400000
                    await Task.Delay(86400000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Worker error: {ex}", ex);
                }
            }
        }
    }
}
