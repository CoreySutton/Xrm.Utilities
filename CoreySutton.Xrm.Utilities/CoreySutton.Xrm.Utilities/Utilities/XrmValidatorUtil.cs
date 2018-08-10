using Microsoft.Xrm.Sdk;

namespace CoreySutton.Xrm.Utilities
{
    public static class XrmValidator
    {
        public static bool IsNullOrEmpty(EntityCollection entityCollection)
        {
            return entityCollection == null ||
                   entityCollection.Entities == null ||
                   entityCollection.Entities.Count == 0;
        }
    }
}
