using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.CommonServices
{
    public class CredentialBusinessService
    {
        private readonly string _launcherConnectionString;
        public CredentialBusinessService(string launcherConnectionString)
        {
            _launcherConnectionString = launcherConnectionString;
        }

        private UserBusinessService _userCredentialBusinessService;

        public UserBusinessService UserCredentialManager =>
            _userCredentialBusinessService ?? (_userCredentialBusinessService =
                new UserBusinessService(_launcherConnectionString));
    }
}
