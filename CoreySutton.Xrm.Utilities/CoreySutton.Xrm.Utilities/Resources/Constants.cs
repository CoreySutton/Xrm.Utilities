using System.Collections.Generic;

namespace CoreySutton.Xrm.Utilities
{
    public static class Constants
    {
        internal const int PageSize = 5000;
        internal const int PageStartNumber = 1;
        internal const int UpdateMultipleBatchSize = 50;
        internal const int ExecuteMultipleBatchSize = 50;
        internal const int StateActiveOsv = 0;
        internal const int StateInactiveOsv = 1;
        internal const int StatusActiveOsv = 1;
        internal const int StatusInactiveOsv = 2;

        public static class Entities
        {
            public static class Calendar
            {
                internal const string EntityLogicalName = "calendar";
            }

            public static class User
            {
                internal const string EntityLogicalName = "systemuser";
            }
        }

        public static class Regions
        {
            public const string None = "None";
            public const string APAC = "APAC";
            public const string CAN = "CAN";
            public const string EMEA = "EMEA";
            public const string IND = "IND";
            public const string JPN = "JPN";
            public const string NorthAmerica = "NorthAmerica";
            public const string NorthAmerica2 = "NorthAmerica2";
            public const string Oceania = "Oceania";
            public const string SouthAmerica = "SouthAmerica";

            public static readonly List<string> List = new List<string> { None, APAC, CAN, EMEA, IND, JPN, NorthAmerica, NorthAmerica2, Oceania, SouthAmerica };
        }
    }
}
