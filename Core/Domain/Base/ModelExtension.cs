using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Base
{
    public static class ModelExtension
    {
        public static DateTime TimestampProvider()
        {
            return DateTime.UtcNow;
        }

      

    }
}
