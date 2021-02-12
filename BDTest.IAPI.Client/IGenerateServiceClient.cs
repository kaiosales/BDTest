using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.IAPI.Client
{
    public interface IGenerateServiceClient
    {
        IAsyncEnumerable<int> GenerateNumbers(int count);
    }
}