using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Configuration
{
    public class AppSettings
    {
        /// <summary>Contains the ConnectionString that determines how to connect to the targeted database engine.</summary>
        /// <remarks>Note: The value is stored unprotected and should be kept secured outside of development!</remarks>
        public string ConnectionString { get; set; }

        
    }
}
