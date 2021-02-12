using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.Domain
{
    public interface IMultiplier
    {
        Task<int> Multiply(int number);
    }
}
