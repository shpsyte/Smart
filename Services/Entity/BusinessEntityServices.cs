using Core.Domain.Business;
using Data.Repository;
using Data.Context;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Entity
{
    public class BusinessEntityServices : Services<BusinessEntity>, IBusinessEntity
    {
        public BusinessEntityServices(SmartContext context, IRepository<BusinessEntity> repository, IUser currentUser, ILogger<Services<BusinessEntity>> logger) : base(context, repository, currentUser, logger)
        {
        }

        public BusinessEntity Check()
        {
            return base.SingleOrDefault();
        }
    }
}
