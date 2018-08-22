using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreySutton.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CoreySutton.Xrm.Utilities
{
    public static partial class IOrganizationServiceExtensions
    {
        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ColumnSet columnSet,
            ConditionExpression[] conditions)
            where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(columnSet, conditions, null);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ColumnSet columnSet,
            LinkEntity[] linkEntities)
            where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(columnSet, null, linkEntities);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ConditionExpression[] conditions,
            LinkEntity[] linkEntities)
           where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(null, conditions, linkEntities);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ColumnSet columnSet)
           where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(columnSet, null, null);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ConditionExpression[] conditions)
           where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(null, conditions, null);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            LinkEntity[] linkEntities)
            where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(null, null, linkEntities);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service)
            where T : Entity, new()
        {
            return service.RetrieveMultiple<T>(null, null, null);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            ColumnSet columnSet,
            ConditionExpression[] conditions,
            LinkEntity[] linkEntities)
            where T : Entity, new()
        {
            var query = new QueryExpression(new T().LogicalName);
            query.ColumnSet = columnSet ?? new ColumnSet(true);

            if (!Validator.IsNullOrEmpty((ICollection)conditions))
                query = QueryExpressionUtil.AddConditionsToQuery(query, conditions);

            if (!Validator.IsNullOrEmpty((ICollection)linkEntities))
                query.LinkEntities.AddRange(linkEntities);

            return service.RetrieveMultiple<T>(query);
        }

        public static List<T> RetrieveMultiple<T>(
            this IOrganizationService service,
            QueryExpression query)
            where T : Entity
        {
            Argument.IsNotNull(query, nameof(query));

            // Top count cannot be specified with paging
            if (query.TopCount == null)
            {
                query.PageInfo = new PagingInfo();
                query.PageInfo.Count = Constants.PageSize;
                query.PageInfo.PageNumber = Constants.PageStartNumber;
                query.PageInfo.PagingCookie = null;
            }

            var entities = new List<T>();

            while (true)
            {
                EntityCollection entityCollection = service.RetrieveMultiple(query);

                if (XrmValidator.IsNullOrEmpty(entityCollection)) return entities;

                entities.AddRange(entityCollection.Entities.Select(e => e.ToEntity<T>()).ToList());

                // Calendar does not support paging, therefore we need a different approach
                if (query.EntityName == Constants.Entities.Calendar.EntityLogicalName &&
                    entityCollection.Entities.Count == Constants.PageSize)
                {
                    List<Guid> retrievedCalendarIds = entities.Select(e => e.Id).ToList();
                    query.Criteria.AddCondition(
                        new ConditionExpression("calendarid", ConditionOperator.NotIn, retrievedCalendarIds));

                    continue;
                }

                // There are more records so get the next page
                if (entityCollection.MoreRecords)
                {
                    query.PageInfo.PageNumber++;
                    query.PageInfo.PagingCookie = entityCollection.PagingCookie;

                    continue;
                }

                // There are no more records to retrieve, so return the results
                return entities;
            }
        }
    }
}