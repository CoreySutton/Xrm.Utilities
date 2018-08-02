using System;
using System.Security;
using CoreySutton.Utilities;

namespace CoreySutton.Xrm.Utilities
{
    public class CrmCredentialManager : ICrmCredentialManager
    {
        private readonly IAppSettingsManager _appSettingsManager;
        private readonly bool _useSavedCredentials = false;
        private readonly bool _canConnectWithSavedCredentials = false;

        private string _username;
        private string _region;
        private string _orgName;
        private SecureString _password;
        private bool _uniqueInstance;
        private bool _useSsl;
        private bool _isOffice365;
        
        public CrmCredentialManager(IAppSettingsManager appSettingsManager)
        {
            _appSettingsManager = appSettingsManager;
            _username = _appSettingsManager.RetrieveUsername();
            _region = _appSettingsManager.RetrieveRegion();
            _orgName = _appSettingsManager.RetrieveOrgName();

            if (GetCanConnectWithSavedCredentials())
            {
                _canConnectWithSavedCredentials = true;
            }

            if (_canConnectWithSavedCredentials)
            {
                if (GetUseSavedCredentials())
                {
                    _useSavedCredentials = true;
                }
            }
        }

        public CrmCredentialManager()
        {
        }

        private bool GetCanConnectWithSavedCredentials()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_region) || string.IsNullOrEmpty(_orgName))
            {
                return false;
            }

            return true;
        }

        private bool GetUseSavedCredentials()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_UseSavedCredentials;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseSavedCredentials_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseSavedCredentials_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public string GetUsername()
        {
            if (_useSavedCredentials && !string.IsNullOrEmpty(_username))
            {
                Console.WriteLine($"Saved Username: {_username}");
                return _username;
            }

            while (true)
            {
                Console.Write(Resources.ConsoleCrmCredentialManager.Prompts.Prompt_Username);

                string inputLine = Console.ReadLine();

                if (string.IsNullOrEmpty(inputLine))
                {
                    Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUsername_Empty);
                }
                else if (ValidateUsername(inputLine))
                {
                    _username = inputLine;
                    return inputLine;
                }
            }
        }

        private bool ValidateUsername(string username)
        {
            // TODO REGEX validation of username

            if (username.IndexOf('@') < 0)
            {
                Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUsername_NoAt);
                return false;
            }

            return true;
        }

        public SecureString GetPassword()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_Password;

            return ConsoleIoUtil.GetSecureString(promptMsg);
        }

        public string GetRegion()
        {
            if (_useSavedCredentials && !string.IsNullOrEmpty(_region))
            {
                Console.WriteLine($"Saved Region: {_region}");
                return _region;
            }

            Console.WriteLine("Region Options");

            for (int i = 0; i < Constants.Regions.List.Count; i++)
            {
                Console.WriteLine($"({i}) {Constants.Regions.List[i]}");
            }

            while (true)
            {
                Console.Write(Resources.ConsoleCrmCredentialManager.Prompts.Prompt_Region, 0, Constants.Regions.List.Count-1);
                string inputLine = Console.ReadLine();

                if (string.IsNullOrEmpty(inputLine))
                {
                    Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidRegion_Empty);
                }
                else if (int.TryParse(inputLine, out int regionOption))
                {
                    if (regionOption >= 0 && regionOption <= Constants.Regions.List.Count)
                    {
                        _region = Constants.Regions.List[regionOption];
                        if (_region == Constants.Regions.None)
                        {
                            _region = string.Empty;
                        }

                        return _region;
                    }
                    else
                    {
                        Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidRegion_OutOfRange);
                    }
                }
                else
                {
                    Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidRegion_ParseFail);
                }
            }
        }

        public string GetOrgName()
        {
            if (_useSavedCredentials && !string.IsNullOrEmpty(_orgName))
            {
                Console.WriteLine($"Saved Organization Name: {_orgName}");
                return _orgName;
            }

            while (true)
            {
                Console.Write(Resources.ConsoleCrmCredentialManager.Prompts.Prompt_OrganizationName);

                string inputLine = Console.ReadLine();

                if (string.IsNullOrEmpty(inputLine))
                {
                    Console.WriteLine(Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidOrganizationName_Empty);
                }
                else
                {
                    _orgName = inputLine;
                    return inputLine;
                }
            }
        }

        public bool GetUniqueInstance()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_UniqueInstance;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUniqueInstance_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUniqueInstance_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public bool GetUseSsl()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_UseSsl;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseSsl_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseSsl_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public bool GetUseAdvancedSetup()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_UseAdvancedSetup;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseAdvancedSetup_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidUseAdvancedSetup_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public bool GetIsOffice365()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_IsOffice365;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidIsOffice365_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidIsOffice365_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public bool GetShouldSaveCredentials()
        {
            string promptMsg = Resources.ConsoleCrmCredentialManager.Prompts.Prompt_ShouldSaveCredentials;
            string emptyErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidShouldSaveCredentials_Empty;
            string notYesNoErrMsg = Resources.ConsoleCrmCredentialManager.Errors.Error_InvalidShouldSaveCredentials_NotYesNo;

            return ConsoleIoUtil.GetYesNo(promptMsg, emptyErrMsg, notYesNoErrMsg);
        }

        public void SaveCredentials()
        {
            _appSettingsManager.SaveUsername(_username);
            _appSettingsManager.SaveOrgName(_orgName);
            _appSettingsManager.SaveRegion(_region);
        }
    }
}
