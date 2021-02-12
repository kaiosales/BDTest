using System;
using System.Threading;
using System.Threading.Tasks;
using BDTest.Core.Models;
using BDTest.IAPI.Events;

namespace BDTest.EAPI.Services
{
    public interface IBatchNumberWorker
    {
        Task DoWork(BatchNumber batchNumber, CancellationToken cancellationToken);
        event EventHandler<NumberProcessedEventArgs> UnitDone;
    }
}