# CoreySutton.Xrm.Utilities

Branch | Build | Release | NuGet
-------|-------|---------|-------
master | [![Build status](https://coreysutton.visualstudio.com/CoreySutton/_apis/build/status/CoreySutton.Xrm.Utilities-CI)](https://coreysutton.visualstudio.com/CoreySutton/_build/latest?definitionId=9) | n/a | [![NuGet version](https://badge.fury.io/nu/coreysutton.xrm.utilities.svg)](https://badge.fury.io/nu/coreysutton.xrm.utilities)
develop | [![Build status](https://coreysutton.visualstudio.com/CoreySutton/_apis/build/status/CoreySutton.Utilities-CI-develop)](https://coreysutton.visualstudio.com/CoreySutton/_build/latest?definitionId=6) | n/a | n/a

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
