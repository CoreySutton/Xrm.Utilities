using System;
using CoreySutton.Utilities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace CoreySutton.Xrm.Utilities
{
    public static partial class IOrganizationServiceExtensions
    {
        public static void ActivateRecord<TEntity>(
            this IOrganizationService organizationService,
            Guid recordId, 
            int status = Constants.StatusActiveOsv)
            where TEntity : Entity, new()
        {
            string entityLogicalName = new TEntity().LogicalName;

            organizationService.ActivateRecord(entityLogicalName, recordId, status);
        }

        public static void ActivateRecord(
            this IOrganizationService organizationService, 
            string entityName, Guid recordId,
            int status = Constants.StatusActiveOsv)
        {
            ArgUtil.NotNull(entityName);

            SetStateRequest setStateRequest = new SetStateRequest
            {
                EntityMoniker = new EntityReference
                {
                    Id = recordId,
                    LogicalName = entityName
                },
                State = new OptionSetValue(Constants.StateActiveOsv),
                Status = new OptionSetValue(status)
            };

            organizationService.Execute(setStateRequest);
        }
        
        public static void DeactivateRecord<TEntity>(
            this IOrganizationService organizationService, 
            Guid recordId,
            int status = Constants.StatusInactiveOsv)
            where TEntity : Entity, new()
        {
            string entityLogicalName = new TEntity().LogicalName;

            organizationService.DeactivateRecord(entityLogicalName, recordId, status);
        }

        public static void DeactivateRecord(
            this IOrganizationService organizationService, 
            string entityName, 
            Guid recordId, 
            int status = Constants.StatusInactiveOsv)
        {
            ArgUtil.NotNull(entityName);

            SetStateRequest setStateRequest = new SetStateRequest
            {
                EntityMoniker = new EntityReference
                {
                    Id = recordId,
                    LogicalName = entityName
                },
                State = new OptionSetValue(Constants.StateInactiveOsv),
                Status = new OptionSetValue(status)
            };

            organizationService.Execute(setStateRequest);
        }
    }
}