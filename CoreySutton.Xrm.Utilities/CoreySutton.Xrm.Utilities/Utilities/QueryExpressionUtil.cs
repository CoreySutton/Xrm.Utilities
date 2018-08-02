using CoreySutton.Utilities;
using Microsoft.Xrm.Sdk.Query;

namespace CoreySutton.Xrm.Utilities
{
    public static class QueryExpressionUtil
    {
        public static QueryExpression AddConditionsToQuery(QueryExpression query, ConditionExpression[] conditions)
        {
            ArgUtil.NotNull(query);
            ArgUtil.NotNull(conditions);

            foreach (ConditionExpression condition in conditions)
            {
                query.Criteria.AddCondition(condition);
            }

            return query;
        }
    }
}
