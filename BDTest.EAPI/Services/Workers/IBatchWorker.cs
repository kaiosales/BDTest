using System;
using System.Threading;
using System.Threading.Tasks;
using BDTest.Core.Models;
using BDTest.IAPI.Events;

namespace BDTest.EAPI.Services
{
    public interface IBatchWorker
    {
        Task DoWork(Batch batch, CancellationToken cancellationToken);
        event EventHandler<NumberProcessedEventArgs> UnitDone;
    }
}