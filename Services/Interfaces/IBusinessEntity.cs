using Core.Domain.Business;

using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IBusinessEntity : IServices<BusinessEntity>
    {
        // If you need to customize your entity actions you can put here    
        BusinessEntity Check();

    }   
}
