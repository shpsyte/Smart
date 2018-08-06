using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Map
{
    public static class Shema
    {
        private const string defaultShema = "Smart";
        private const bool useMySQL = true;


        public static string shemaSecurity = useMySQL ? defaultShema : "Security";
        public static string shemaBusiness = useMySQL ? defaultShema : "Business";
        public static string shemaPerson = useMySQL ? defaultShema : "Person";
        public static string shemaAccounting = useMySQL ? defaultShema : "Accounting";
        public static string shemaFinancial = useMySQL ? defaultShema : "Financial";
        public static string shemaProduction = useMySQL ? defaultShema : "Production";
        public static string shemaSales = useMySQL ? defaultShema : "Production";
    }
}
