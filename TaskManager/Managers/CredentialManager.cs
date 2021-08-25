using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Global;
using TaskManager.Services.CommonServices;

namespace TaskManager.Managers
{
    public class CredentialManager
    {
        private readonly CredentialBusinessService _credentialBusinessService;

        public CredentialManager()
        {
            _credentialBusinessService = new CredentialBusinessService(SessionVars.ConnectionString);
        }

        public UserBusinessService UserCredentialManager => _credentialBusinessService.UserCredentialManager;
    }
}
