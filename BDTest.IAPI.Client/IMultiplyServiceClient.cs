using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.IAPI.Client
{
    public interface IMultiplyServiceClient
    {
        Task<int> MultiplyNumber(int number);
    }
}