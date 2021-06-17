using System.Collections.Generic;

namespace CoreySutton.Xrm.Utilities
{
    public static class Constants
    {
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
