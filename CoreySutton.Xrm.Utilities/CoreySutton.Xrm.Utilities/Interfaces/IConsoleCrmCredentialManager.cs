using System.Security;

namespace CoreySutton.Xrm.Utilities
{
    public interface ICrmCredentialManager
    {
        string GetUsername();
        SecureString GetPassword();
        string GetRegion();
        string GetOrgName();
        bool GetUniqueInstance();
        bool GetUseSsl();
        bool GetUseAdvancedSetup();
        bool GetIsOffice365();
        bool GetShouldSaveCredentials();
        void SaveCredentials();
    }
}