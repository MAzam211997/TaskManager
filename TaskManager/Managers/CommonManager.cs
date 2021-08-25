using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Global;
using TaskManager.Services.CommonServices;

namespace TaskManager.Managers
{
    public class CommonManager
    {
        private readonly CommonBusinessService _commonBusinessService;

        public CommonManager()
        {
            _commonBusinessService = new CommonBusinessService(SessionVars.ConnectionString, SessionVars.LoggedInLoginID);
        }
        public IDbTransaction DBTransaction
        {
            set => _commonBusinessService.DBTransaction = value;
            get => _commonBusinessService.DBTransaction;
        }
        public UserBusinessService UserManager => _commonBusinessService.UserManager;
        //public RoleBusinessService RolesManager => _commonBusinessService.RolesManager;
        //public DeliverableService DeliverableManager => _commonBusinessService.DeliverableManager;
        //public DeliverableAssignmentService DeliverableAssignmentManager => _commonBusinessService.DeliverableAssignmentManager;
        //public DeliverableCommentsService DeliverableCommentsManager => _commonBusinessService.DeliverableCommentsManager;
        //public LetterheadService LetterheadManager => _commonBusinessService.LetterheadManager;
        //public DeliverableAssignmentAdminService DeliverableAssignmentAdminManager => _commonBusinessService.DeliverableAssignmentAdminManager;

    }
}
