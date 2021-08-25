using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TaskManager.Services.CommonServices
{
   public class CommonBusinessService
    {
        private readonly string _clientConnectionString;
        private readonly int _userID;
        private IDbTransaction _txn;

        public CommonBusinessService(string clientConnectionString, int userID)
        {
            _clientConnectionString = clientConnectionString;
            _userID = userID;
        }
        public IDbTransaction DBTransaction
        {
            set => _txn = value;
            get => _txn;
        }
    }
}
