namespace CoreySutton.Xrm.Utilities
{
    public class QueryExceptionDetails
    {
        private const string MoreThanOneRecordFoundMessageDefault = "More than one record found meeting search criteria.";
        private const string NoRecordsFoundMessageDefault = "There were no records found meeting search criteria";

        private bool ErrorOnMoreThanOneRecordFound { get; }
        private bool ErrorOnNoRecordsFound { get; }
        private string MoreThanOneRecordFoundMessage { get; }
        private string NoRecordsFoundMessage { get; }


        public QueryExceptionDetails(
            bool errorOnMoreThanOne = false,
            bool errorOnNone = false,
            string moreThanOneMessage = MoreThanOneRecordFoundMessageDefault,
            string noneMessage = NoRecordsFoundMessageDefault)
        {
            ErrorOnMoreThanOneRecordFound = errorOnMoreThanOne;
            ErrorOnNoRecordsFound = errorOnNone;
            MoreThanOneRecordFoundMessage = moreThanOneMessage;
            NoRecordsFoundMessage = noneMessage;
        }
    }

    /// <summary>
    /// The IOrganizationServiceExtensions class simplifies operations using IOrganizationService. It simplifies Crm 
    /// calls through generics, and removes the need for building query expressions boilerplate.
    /// </summary>
    public static partial class IOrganizationServiceExtensions
    {
    }
}