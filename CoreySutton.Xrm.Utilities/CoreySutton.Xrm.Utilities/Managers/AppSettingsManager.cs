using System;
using System.Configuration;
using CoreySutton.Utilities;

namespace CoreySutton.Xrm.Utilities
{
    public class AppSettingsManager : IAppSettingsManager
    {
        private readonly ApplicationSettingsBase _settings;

        public AppSettingsManager(ApplicationSettingsBase settings)
        {
            _settings = settings;
        }

        public string RetrieveDecryptedSetting(string settingName, string entropyName)
        {
            if (_settings[settingName] == null)
            {
                Console.WriteLine($"App does not contain setting {settingName}");
                return null;
            }

            if (!(_settings[settingName] is string))
            {
                Console.WriteLine($"Setting {settingName} is not of type string");
                return null;
            }

            if (string.IsNullOrEmpty(_settings[settingName] as string))
            {
                Console.WriteLine($"Setting {settingName} is null or empty");
                return null;
            }

            if (_settings[entropyName] == null)
            {
                Console.WriteLine($"App does not contain setting {entropyName}");
                return null;
            }

            if (!(_settings[entropyName] is string))
            {
                Console.WriteLine($"Setting {entropyName} is not of type string");
                return null;
            }

            if (string.IsNullOrEmpty(_settings[entropyName] as string))
            {
                Console.WriteLine($"Setting {entropyName} is null or empty");
                return null;
            }

            string cypherText = _settings[settingName] as string;
            string entropy = _settings[entropyName] as string;

            return EncryptionUtil.Decrypt(cypherText, entropy);
        }

        public string RetrieveUsername()
        {
            return RetrieveDecryptedSetting("Username", "UsernameEntropy");
        }

        public string RetrieveRegion()
        {
            return RetrieveDecryptedSetting("Region", "RegionEntropy");
        }

        public string RetrieveOrgName()
        {
            return RetrieveDecryptedSetting("OrganizationName", "OrganizationNameEntropy");
        }

        public void SaveUnencryptedSetting(string settingName, string settingEntropyName, string plainTextValue)
        {
            (string cypherText, string entropy) = EncryptionUtil.Encrypt(plainTextValue);

            _settings[settingName]= cypherText;
            _settings[settingEntropyName] = entropy;

            _settings.Save();
        }

        public void SaveUsername(string username)
        {
            SaveUnencryptedSetting("Username", "UsernameEntropy", username);
        }

        public void SaveRegion(string region)
        {
            SaveUnencryptedSetting("Region", "RegionEntropy", region);
        }

        public void SaveOrgName(string organizationName)
        {
            SaveUnencryptedSetting("OrganizationName", "OrganizationNameEntropy", organizationName);
        }
    }
}
