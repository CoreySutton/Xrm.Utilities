# CoreySutton.Xrm.Utilities

[![Build Status](https://travis-ci.org/CoreySutton/Xrm.Utilities.svg?branch=master)](https://travis-ci.org/CoreySutton/Xrm.Utilities)

## Extensions
Extention class for `IOrganizationService` interface.
* CRUD / Execute / SetState extention methods.
  * RetrieveMultiple handles paging
  * Supports returning earlybound types

## Utilities
### CrmConnectorUtil
`IOrganizationService Connect(string connectionString)`

`IOrganizationService Connect(ICrmCredentialManager crmCredentialManager)`

Connect to CRM using a connection string or prompt the user for credentials.

### QueryExpressionUtil
* `AddConditionsToQuery()`

Add an array of ConditionExpressions to a QueryExpression

### XrmValidatorUtil
* `IsNullOrEmpty(EntityCollection entity Collection)`

Validate if EntityColleciton contains  at least one value.
