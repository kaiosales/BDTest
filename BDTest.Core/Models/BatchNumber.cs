using System;
using System.Collections.Generic;

namespace BDTest.Core.Models
{
    public class BatchNumber : BaseEntity
    {
        public long BatchId { get; set; }
        public Batch Batch { get; set; }
        public int Number { get; set; }
        public int Product { get; set; }
    }
}