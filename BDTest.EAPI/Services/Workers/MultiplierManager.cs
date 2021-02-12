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
    public class MultiplierManager : IBatchNumberWorker
    {
        private IMultiplyServiceClient _client;
        private IServiceScopeFactory _scopeFactory;
        public event EventHandler<NumberProcessedEventArgs> UnitDone;

        public MultiplierManager(IMultiplyServiceClient client, IServiceScopeFactory scopeFactory)
        {
            _client = client;
            _scopeFactory = scopeFactory;
        }

        public async Task DoWork(BatchNumber batchNumber, CancellationToken cancellationToken)
        {
            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                IRepository<BatchNumber> batchNumberRepository = scope.ServiceProvider.GetRequiredService<IRepository<BatchNumber>>();

                await batchNumberRepository.ReloadAsync(batchNumber);

                batchNumber.Product = await _client.MultiplyNumber(batchNumber.Number);
                await batchNumberRepository.UpdateAsync(batchNumber);

                await Task.Run(() => UnitDone?.Invoke(this, new NumberProcessedEventArgs(batchNumber, cancellationToken)));
            }

        }
    }
}