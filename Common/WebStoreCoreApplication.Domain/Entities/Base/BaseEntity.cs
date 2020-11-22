using System;
using System.Collections.Generic;
using System.Text;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
