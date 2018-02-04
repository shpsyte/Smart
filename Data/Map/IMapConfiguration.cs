using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Map
{
    public interface IMapConfiguration<T> where T:class
    {
        void Map(EntityTypeBuilder<T> entity);
    }
}
