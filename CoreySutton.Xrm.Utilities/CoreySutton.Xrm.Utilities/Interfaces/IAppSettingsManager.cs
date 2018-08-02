namespace CoreySutton.Xrm.Utilities
{
    public interface IAppSettingsManager
    {
        string RetrieveDecryptedSetting(string settingName, string entropyName);
        string RetrieveOrgName();
        string RetrieveRegion();
        string RetrieveUsername();
        void SaveOrgName(string organizationName);
        void SaveRegion(string region);
        void SaveUnencryptedSetting(string settingName, string settingEntropyName, string plainTextValue);
        void SaveUsername(string username);
    }
}