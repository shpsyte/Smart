using Core.Domain.Region;
using Data.Context;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Entity;
using Services.Interfaces;
using Smart.Controllers;
using Smart.Services;
using System;
using Xunit;

namespace Smart.Test
{
    public class UnitTest1
    {
      
         

        [Fact]
        public void Test1()
        {
            var Cidade = new City()
            {
                BusinessEntityId = 1,
                MiddleName = "",
                Name = "",
                SpecialCodeRegion = "",
                StateProvinceId = 0
            };

         



    }
    }
}
