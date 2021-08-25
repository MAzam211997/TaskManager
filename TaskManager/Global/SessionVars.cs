
namespace TaskManager.Global
{
    public sealed class SessionVars
    {

        #region Private Constants- Loggedin Vars
        //---------------------------------------------------------------------
        private const string browserDate = "BrowserDate";
        private const string loggedInUserDisplayName = "LoggedInUserDisplayName";
        private const string connectionString = "ConnectionString";
        private const string loggedInLoginID = "LoggedInLoginID";
        private const string loggedInRoleID = "LoggedInRoleID";
        private const string logedInUserEmail = "LogedInUserEmail";
        private const string logedInUserMobile = "LogedInUserMobile";
        private const string logedInInitial = "LogedInInitial";

        //---------------------------------------------------------------------
        # endregion

        # region Private Constants- RISKY
        //---------------------------------------------------------------------
        private const string isRTL = "IsRTL";

        //---------------------------------------------------------------------
        #endregion

        #region Public Properties - Loggedin Vars

        public static string BrowserDate
        {
            set => AppContext.Current.Session.SetValue(browserDate, value);
            get => AppContext.Current.Session.GetValue<string>(browserDate) ?? string.Empty;
        }

        public static string LoggedInUserDisplayName
        {
            set => AppContext.Current.Session.SetValue(loggedInUserDisplayName, value);
            get => AppContext.Current.Session.GetValue<string>(loggedInUserDisplayName) ?? string.Empty;
        }
        public static string ConnectionString
        {
            set => AppContext.Current.Session.SetValue(connectionString, value);
            get => AppContext.Current.Session.GetValue<string>(connectionString) ?? string.Empty;
        }

        public static int LoggedInLoginID
        {
            set => AppContext.Current.Session.SetValue(loggedInLoginID, value);
            get => AppContext.Current.Session.GetValue<int>(loggedInLoginID) != 0 ? AppContext.Current.Session.GetValue<int>(loggedInLoginID) : 0;
        }

        public static int LoggedInRoleID
        {
            set => AppContext.Current.Session.SetValue(loggedInRoleID, value);
            get => AppContext.Current.Session.GetValue<int>(loggedInRoleID) != 0 ? AppContext.Current.Session.GetValue<int>(loggedInRoleID) : 0;
        }
        public static string LogedInUserEmail
        {
            set => AppContext.Current.Session.SetValue(logedInUserEmail, value);
            get => AppContext.Current.Session.GetValue<string>(logedInUserEmail) ?? string.Empty;
        }
        public static string LogedInInitial
        {
            set => AppContext.Current.Session.SetValue(logedInInitial, value);
            get => AppContext.Current.Session.GetValue<string>(logedInInitial) ?? string.Empty;
        }
        public static string LogedInUserMobile
        {
            set => AppContext.Current.Session.SetValue(logedInUserMobile, value);
            get => AppContext.Current.Session.GetValue<string>(logedInUserMobile) ?? string.Empty;
        }
        #endregion





        #region Public Properties - RISKY

        public static bool IsRTL
        {
            set => AppContext.Current.Session.SetValue(isRTL, value);
            get => AppContext.Current.Session.GetValue<bool>(isRTL) && AppContext.Current.Session.GetValue<bool>(isRTL);
        }
        #endregion


        #region Public Properties - Clear Seesion, Abandon

        public static void ForceLogout()
        {
            AppContext.Current.Session.Clear();
        }

        public static void ForceLogoutUser()
        {
            AppContext.Current.Session.Clear();
        }

        public static void ForceSessionClear()
        {
            AppContext.Current.Session.Clear();
        }

        #endregion
    }
}

