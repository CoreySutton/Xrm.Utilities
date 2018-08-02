using System;
using System.Security;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Tooling.Connector;

namespace CoreySutton.Xrm.Utilities
{
    public static class CrmConnectorUtil
    {
        public static IOrganizationService Connect(ICrmCredentialManager crmCredentialManager)
        {
            Console.WriteLine("Please provide credentails to connect to CRM");

            while (true)
            {
                string username = crmCredentialManager.GetUsername();
                SecureString password = crmCredentialManager.GetPassword();

                string region = crmCredentialManager.GetRegion();
                string orgName = crmCredentialManager.GetOrgName();

                bool uniqueInstance = false;
                bool useSsl = false;
                bool isOffice365 = true;
                if (crmCredentialManager.GetUseAdvancedSetup())
                {
                    uniqueInstance = crmCredentialManager.GetUniqueInstance();
                    useSsl = crmCredentialManager.GetUseSsl();
                    crmCredentialManager.GetIsOffice365();
                }

                if (crmCredentialManager.GetShouldSaveCredentials())
                {
                    crmCredentialManager.SaveCredentials();
                }

                IOrganizationService organizationService = GetOrganizationService(username, password, region, orgName, null, uniqueInstance, useSsl, isOffice365);
                if (organizationService != null) return organizationService;

                Console.WriteLine("Please try again");
                Console.WriteLine();
            }
        }

        private static IOrganizationService GetOrganizationService(
            string username,
            SecureString password,
            string region,
            string orgName,
            OrganizationDetail orgDetail = null,
            bool uniqueInstance = false,
            bool useSsl = false,
            bool isOffice365 = true)
        {
            // TODO test this with online V9, V8
            // TODO test this with on-prem and on-prem IFD
            // TODO test this with EVERYTHING
            Console.Write("Initializing connection to Dynamics 365...");

            CrmServiceClient crmSvc = null;
            Exception connectionException = null;
            try
            {
                crmSvc = new CrmServiceClient(
                    crmUserId: username,
                    crmPassword: password,
                    crmRegion: region,
                    orgName: orgName,
                    orgDetail: null,
                    useUniqueInstance: uniqueInstance,
                    useSsl: useSsl,
                    isOffice365: isOffice365);
            }
            catch (Exception ex)
            {
                connectionException = ex;
            }

            // Handle success
            if (crmSvc != null && crmSvc.OrganizationServiceProxy != null && crmSvc.IsReady == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[success]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();

                return crmSvc.OrganizationServiceProxy;
            }

            // Handle error
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed]");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (connectionException != null)
            {
                Console.WriteLine($"Failed to connect to CRM: {connectionException.Message}");
            }

            if (crmSvc != null)
            {
                if (!string.IsNullOrEmpty(crmSvc.LastCrmError))
                {
                    Console.WriteLine($"Last Crm Error: {crmSvc.LastCrmError}");
                }
                if (crmSvc.LastCrmException != null)
                {
                    Console.WriteLine($"Last Crm Exception Message: {crmSvc.LastCrmException.Message}");
                    Console.WriteLine($"Last Crm Exception Stack Trace: {crmSvc.LastCrmException.StackTrace}");
                }
            }

            return null;
        }
    }
}
