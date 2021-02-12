using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.Domain
{
    public class From1to100Generator : IGenerator
    {
        private Random _rnd = new Random();
        
        public async IAsyncEnumerable<int> Generate(int count)
        {
            for (var i = 0; i < count; i++) 
            {
                await Task.Delay(TimeSpan.FromSeconds(_rnd.Next(5, 10)));
                yield return _rnd.Next(1, 100);
            }
        }
    }
}
