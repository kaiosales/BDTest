using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDTest.Domain
{
    public class RandomMultiplier : IMultiplier
    {
        private Random _rnd = new Random();
        
        public async Task<int> Multiply(int number)
        {
            await Task.Delay(TimeSpan.FromSeconds(_rnd.Next(5, 10)));
            return number * _rnd.Next(2, 4);
        }
    }
}
