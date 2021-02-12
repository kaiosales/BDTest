using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BDTest.Core.Models;
using BDTest.Data;
using BDTest.IAPI.Client;
using BDTest.IAPI.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BDTest.EAPI.Services
{
    public class GeneratorManager : IBatchWorker
    {
        private IGenerateServiceClient _client;
        private IServiceScopeFactory _scopeFactory;
        public event EventHandler<NumberProcessedEventArgs> UnitDone;

        public GeneratorManager(IGenerateServiceClient client, IServiceScopeFactory scopeFactory)
        {
            _client = client;
            _scopeFactory = scopeFactory;
        }

        public async Task DoWork(Batch batch, CancellationToken cancellationToken)
        {
            await foreach (int number in _client.GenerateNumbers(batch.Count))
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                using(IServiceScope scope = _scopeFactory.CreateScope())
                {
                    IRepository<BatchNumber> batchNumberRepository = scope.ServiceProvider.GetRequiredService<IRepository<BatchNumber>>();
                    BatchNumber batchNumber = new BatchNumber { BatchId = batch.Id, Number = number };
                    await batchNumberRepository.InsertAsync(batchNumber);
                    
                    await Task.Run(() => UnitDone?.Invoke(this, new NumberProcessedEventArgs(batchNumber, cancellationToken)));
                }
            }
        }
    }
}