using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Views
{
    public static class ModelCards
    {
        public static string SimpleNumber => "OnlyNumber";
        public static string SimpleNumberUnFormat => "OnlyNumberUnFormat";
        public static string SimpleCard => "SimpleCard";
        public static string SimpleCardTitle => "SimpleCardTitle";

        public static string SimpleTableRow => "SimpleTableRow";


        public static int BankAmountBefore => 0;
        public static int BankAmountTotal => 1;

        public static int ToLate => 0;
        public static int ToDay => 1;
        public static int ToAfter => 2;
        public static int All => 3;
        public static int Prevision => 4;
        public static int OnlyQtdy => 5;
    }
}
