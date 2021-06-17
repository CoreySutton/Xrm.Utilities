using System;
using CoreySutton.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CoreySutton.Xrm.Utilities
{
    public static partial class IOrganizationServiceExtensions
    {
        public static T Retrieve<T>(
            this IOrganizationService service,
            Guid entityId,
            ColumnSet columnSet = null)
            where T : Entity, new()
        {
            Argument.IsNotNull(entityId, nameof(entityId));

            Entity entity = service.Retrieve(new T().LogicalName, entityId, columnSet ?? new ColumnSet(true));

            return entity.ToEntity<T>();
        }
    }
}