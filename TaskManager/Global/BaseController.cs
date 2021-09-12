using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TaskManager.Managers;
using TaskManager.ViewModel;

namespace TaskManager.Global
{
    public class BaseController : Controller
    {

        public enum Messages { Red, Green, White };
        public enum Modes { Default, Insert, Update, ReadOnly, Delete, Unknown };
        public enum NavigationTypes { first = 1, previous = 2, next = 3, last = 4 };
        public enum SaveTypes { SaveNew, SaveUpdate };

        #region Random

        public static string DecodeStringToBase64UTF8(string encodedString)
        {
            if (!string.IsNullOrEmpty(encodedString))
            {
                byte[] nameString = Convert.FromBase64String(encodedString);

                return HttpUtility.UrlDecode(Encoding.UTF8.GetString(nameString));
            }
            return encodedString;
        }



        public static string ConcatenateString(string string1, string string2, string seperator = " ")
        {
            string formattedstring = string.Empty;

            if (SessionVars.IsRTL)
            {
                string LRM = ((char)0x200E).ToString();
                //for resolving numeric RTL Issue                
                formattedstring = string1 + seperator + LRM + string2 + LRM;
            }
            else
                formattedstring = string1 + seperator + string2;

            return formattedstring;
        }

        #endregion

        #region Managers Singleton

        protected ResponseViewModel _responseViewModel;
        protected ResponseMessages _responseMessages = new ResponseMessages();

        private CredentialManager _credentialManager;
        protected CredentialManager CredentialManager => _credentialManager ?? (_credentialManager = new CredentialManager());

        private CommonManager _commonManager;
        protected CommonManager CommonManager => _commonManager ?? (_commonManager = new CommonManager());

        private GlobalManager _globalManager;
        protected GlobalManager GlobalManager => _globalManager ?? (_globalManager = new GlobalManager());
        #endregion

    }
}
