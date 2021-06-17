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
    }
}
