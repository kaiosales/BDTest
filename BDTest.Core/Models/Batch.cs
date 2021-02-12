using System;
using System.Collections.Generic;

namespace BDTest.Core.Models
{
    public class Batch : BaseEntity
    {
        public int Count { get; set; }
        public StatusEnum Status { get; set; }
        public int Total { get; set; }
        public List<BatchNumber> Numbers { get; set; }
    }
}