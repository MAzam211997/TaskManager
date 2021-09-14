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
        private UserBusinessService _userBusinessService;

        public UserBusinessService UsersManager =>
            _userBusinessService ?? (_userBusinessService =
                new UserBusinessService(_clientConnectionString));

        private RolesBusinessService _rolesBusinessService;
        public RolesBusinessService RolesManager =>
            _rolesBusinessService ?? (_rolesBusinessService =
                new RolesBusinessService(_clientConnectionString));

        private TasksBusinessService _tasksBusinessService;
        public TasksBusinessService TasksManager =>
            _tasksBusinessService ?? (_tasksBusinessService =
                new TasksBusinessService(_clientConnectionString));
    }
}
