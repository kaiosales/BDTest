using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using BDTest.Core.Models;
using BDTest.Data;
using BDTest.IAPI.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BDTest.EAPI.Services
{
    public class ProcessorBackgroundService: BackgroundService
    {
        private ILogger<ProcessorBackgroundService> _logger;
        private IServiceScopeFactory _scopeFactory;
        private IBatchWorker _generatorManager;
        private IBatchNumberWorker _multiplierManager;

        public ProcessorBackgroundService(
            ILogger<ProcessorBackgroundService> logger, 
            IServiceScopeFactory scopeFactory,
            IBatchWorker generatorManager,
            IBatchNumberWorker multiplierManager
        )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            
            _generatorManager = generatorManager;
            _generatorManager.UnitDone += NumberGenerated;

            _multiplierManager = multiplierManager;
            _multiplierManager.UnitDone += NumberMultiplied;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Process(cancellationToken);
                    await Task.Delay(1000);
                }
                catch (Exception) {}
            }
        }

        private Task Process(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching for batches to process...");


            IEnumerable<Batch> batches = Enumerable.Empty<Batch>();

            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                IRepository<Batch> batchRepository = scope.ServiceProvider.GetRequiredService<IRepository<Batch>>();
                batches = batchRepository.GetAll(r => r.Status == StatusEnum.Pending).ToList();
            }

            _logger.LogInformation($"{batches.Count()} batches found");

            Parallel.ForEach(batches, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 }, async batch =>
            {
                _logger.LogInformation($"Processing batch {batch.Id}");

                using(IServiceScope scope = _scopeFactory.CreateScope())
                {
                    IRepository<Batch> batchRepository = scope.ServiceProvider.GetRequiredService<IRepository<Batch>>();
                    await batchRepository.ReloadAsync(batch);

                    batch.Status = StatusEnum.Processing;
                    await batchRepository.UpdateAsync(batch);
                }
                
                await _generatorManager.DoWork(batch, cancellationToken);
            });

            return Task.CompletedTask;
        }

        private async void NumberGenerated(object sender, NumberProcessedEventArgs e)
        {
            _logger.LogInformation($"Number {e.BatchNumber.Number} generated for batch {e.BatchNumber.BatchId}");
            await _multiplierManager.DoWork(e.BatchNumber, e.CancellationToken);
        }

        private async void NumberMultiplied(object sender, NumberProcessedEventArgs e)
        {
            _logger.LogInformation($"Number {e.BatchNumber.Number} multiplied for batch {e.BatchNumber.BatchId}");

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IRepository<Batch> batchRepository = scope.ServiceProvider.GetRequiredService<IRepository<Batch>>();
                Batch batch = await batchRepository.GetByIdAsyncIncluding(e.BatchNumber.BatchId, b => b.Numbers);

                batch.Total += e.BatchNumber.Product;

                if (batch.Numbers.Count == batch.Count)
                    batch.Status = StatusEnum.Done;
                
                await batchRepository.UpdateAsync(batch);
            }

        }
    }
}
