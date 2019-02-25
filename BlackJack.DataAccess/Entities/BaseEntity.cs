using Dapper.Contrib.Extensions;
using System;

namespace BlackJack.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
