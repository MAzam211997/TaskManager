using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Global;
using TaskManager.Services.CommonServices;

namespace TaskManager.Managers
{
    public class GlobalManager
    {
        private readonly string _clientConnectionString;

        private GlobalBusinessService _globalBusinessService;

        public GlobalManager()
        {
            _clientConnectionString = SessionVars.ConnectionString;

        }

        public IDbTransaction DBTransaction { set; get; }

        public GlobalBusinessService GeneralManager
        {
            get
            {
                if (_globalBusinessService == null)
                    _globalBusinessService = new GlobalBusinessService(_clientConnectionString);

                return _globalBusinessService;
            }
        }

    }

}
