using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.models
{
    public abstract class BaseEntity 
    {
        public BaseEntity(Guid? id=null)
        {
            Id = id;
        }
        public Guid? Id { get; set; }
        
    }
}
