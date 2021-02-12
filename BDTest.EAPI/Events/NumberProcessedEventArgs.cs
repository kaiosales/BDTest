
using System;
using System.Threading;
using BDTest.Core.Models;

namespace BDTest.IAPI.Events
{
    public class NumberProcessedEventArgs : EventArgs
    {  
        public BatchNumber BatchNumber { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public NumberProcessedEventArgs(BatchNumber batchNumber, CancellationToken cancellationToken)
        {
            BatchNumber = batchNumber;
            CancellationToken = cancellationToken;
        }
    }
}  