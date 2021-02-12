using System;

namespace BDTest.Core.Models
{
    public class BaseEntity : IEntity
    {
        public long Id { get; set; }
    }
}