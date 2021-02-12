using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.Domain
{
    public interface IGenerator
    {
        IAsyncEnumerable<int> Generate(int count);
    }
}
