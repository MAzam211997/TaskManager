using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TaskManager.Entities.GeneralModels
{
    public class AuditAppLib
    {
        private const int baseStart = 1900;
        private const int baseEnd = 2999;
        private const string baseCultureCode = "en-US";
        private readonly Calendar baseCalendar;
        private readonly CultureInfo baseCulture;

        //Local is Arabic
        private const int localStart = 1300;
        private const int localEnd = 1899;
        private const string localCultureCode = "ar-SA";
        private readonly Calendar localCalendar;
        private readonly CultureInfo localCulture;

        //User Preferences 
        //private const string strLoggedInCalendarCode = "LoggedInCalendarCode";

        public string ConvertToArabicNumerals(string strEng, int variant = 1)
        {
            char[] easternArabic = { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

            char[] subContinentArabic = { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

            string ret = "";

            if (variant == 1)
            {
                foreach (char c in strEng)
                    ret += easternArabic[int.Parse(c.ToString())];
            }
            else
            {
                foreach (char c in strEng)
                    ret += subContinentArabic[int.Parse(c.ToString())];
            }

            return ret;
        }

        public AuditAppLib()
        {

            //Init Base
            baseCalendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
            baseCulture = new CultureInfo(baseCultureCode);

            //Init Local
            localCalendar = new HijriCalendar();       // UmAlQuraCalendar();
            localCulture = new CultureInfo(localCultureCode) { DateTimeFormat = { Calendar = localCalendar } };
        }
        public Guid GetNewGuid()
        {
            Guid g;
            g = Guid.NewGuid();
            return g;
        }
        public DataTable Amount_ConvertMultipleAmountsToDisplayFormat(DataTable dt, string amountColumnName, int decimalsInAmount, int profileAmountFormat)
        {
            DataTable ret = dt.Clone();
            ret.Columns[amountColumnName].DataType = typeof(string);

            foreach (DataRow dr in dt.Rows)
                ret.ImportRow(dr);

            foreach (DataRow dr in ret.Rows)
            {
                var amount = decimal.Parse(dr[amountColumnName].ToString());
                dr[amountColumnName] = Amount_DisplayFormat(amount, decimalsInAmount, profileAmountFormat);
            }

            return ret;
        }

        public DataTable Date_ConvertMultipleDatesToDisplayFormat(DataTable dt, string dateColumnNames, string loggedInCalendarCode)
        {
            DataTable ret = dt.Clone();

            foreach (DataRow dr in dt.Rows)
                ret.ImportRow(dr);

            string[] dateColumnSplits = dateColumnNames.Split(new[] { ',' });

            foreach (string d in dateColumnSplits)
            {
                foreach (DataRow dr in ret.Rows)
                {
                    dr[d] = Date_DisplayFormat((string)dr[d], loggedInCalendarCode);
                }
            }
            return ret;
        }

        /// <summary>
        /// Returns flat 8 characters without separators such as 20120924 in yyyymmdd format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string Date_SaveFormat(string date)
        {
            string retDate = string.Empty;

            string formattedDate = FormatDateString(date);

            if (!string.IsNullOrEmpty(formattedDate))
            {
                formattedDate = ConvertToBaseCalendar(formattedDate);
                retDate = formattedDate.Substring(6, 4) + formattedDate.Substring(3, 2) + formattedDate.Substring(0, 2);
            }

            return retDate;
        }

        /// <summary>
        /// We are using this method in WebAPI
        /// </summary>
        /// <param name="date"></param>
        /// <param name="loggedInCalendarCode"></param>
        /// <returns></returns>
        public string Date_DisplayFormat(string date, string loggedInCalendarCode)
        {
            string showDt = FormatDateString(date);
            if (!string.IsNullOrEmpty(showDt))
            {
                //if (loggedInCalendarCode != baseCultureCode)
                if ("en-US" != baseCultureCode)
                    showDt = ConvertToLocalCalendar(showDt);
            }
            return showDt;
        }

        private bool IsDateStringValid(string date, int validOption)
        {
            if (validOption == 0)       // Nothing to be validated
                return true;

            string formattedDate = FormatDateString(date);  // Check Format is Ok? & Convert to standard format

            if (string.IsNullOrEmpty(formattedDate))
                return false;

            if (validOption == 1)       //  Option 1 Ends == Format is Okay
                return true;

            return true;
        }

        public string Date_DisplayFormatCurrentDate(string browserDate, string loggedInCalendarCode)
        {
            string retDate = string.Empty;
            string tDate = browserDate;
            if (!string.IsNullOrEmpty(tDate))
                retDate = Date_DisplayFormat(tDate, loggedInCalendarCode);

            return retDate;
        }

        public string Date_SaveFormatCurrentDate(string browserDate)
        {
            return Date_SaveFormat(browserDate);
        }

        private string FormatDateString(string date)
        {
            string retValue = string.Empty;

            if (string.IsNullOrEmpty(date))
                return retValue;

            string[] delimArray = new string[] { "/", ".", "-" };
            string[] dateVar = new string[3] { "", "", "" };
            string delimiter = string.Empty;

            var mm = 0;
            int yy = 0;

            date = date.Trim();

            foreach (string tmp in delimArray)
            {
                if (!string.IsNullOrEmpty(tmp))
                {
                    if (date.IndexOf(tmp, StringComparison.Ordinal) != -1)
                    {
                        delimiter = tmp;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(delimiter))
            {
                date += delimiter;

                int i = 0, j = 0, k = 0;
                while ((i = date.IndexOf(delimiter, i, StringComparison.Ordinal)) != -1)
                {
                    string s = date.Substring(j, i - j);
                    dateVar[k] = s;
                    j = i + 1; i++; k++;
                }
            }
            else
            {
                if (date.Length != 8)
                    return retValue;

                dateVar[2] = date.Substring(0, 4);
                dateVar[1] = date.Substring(4, 2);
                dateVar[0] = date.Substring(6, 2);
            }

            dateVar[2] = dateVar[2].Trim();
            dateVar[1] = dateVar[1].Trim();
            dateVar[0] = dateVar[0].Trim();

            if (!IsNumeric(dateVar[0]) || !IsNumeric(dateVar[1]) || !IsNumeric(dateVar[2]))
                return retValue;

            if (dateVar[2].Length == 2)
                dateVar[2] = "20" + dateVar[2];

            var dd = Convert.ToInt32(dateVar[0]);
            mm = Convert.ToInt32(dateVar[1]);
            yy = Convert.ToInt32(dateVar[2]);

            if ((dd == 0) || (mm == 0) || (yy == 0))
                return "00/00/0000";

            int calType = 0;
            if (yy >= baseStart && yy <= baseEnd)
                calType = 1;

            if (calType == 0)
            {
                if (yy >= localStart && yy <= localEnd)
                    calType = 2;
            }

            if (calType == 0)
                return retValue;

            var calender = calType == 2 ? localCalendar : baseCalendar;

            int dys = 0;
            var mths = calender.GetMonthsInYear(yy);
            if (mm <= mths)
                dys = calender.GetDaysInMonth(yy, mm);

            if (dys == 0 || mths == 0)
                return retValue;

            if ((dd < 1 || dd > dys) || (mm < 1 || mm > mths))
                return retValue;

            retValue = dd.ToString().PadLeft(2, '0') + "/" + mm.ToString().PadLeft(2, '0') + "/" + yy.ToString().PadLeft(4, '0');
            return retValue;
        }

        public int GetTotalDaysInMonth(string date)
        {
            int retValue = 0;

            if (string.IsNullOrEmpty(date))
                return retValue;

            string[] delimArray = new string[] { "/", ".", "-" };
            string[] dateVar = new string[3] { "", "", "" };
            string delimiter = string.Empty;

            int DD = 0, MM = 0, YY = 0;

            date = date.Trim();

            foreach (string tmp in delimArray)
            {
                if (!string.IsNullOrEmpty(tmp))
                {
                    if (date.IndexOf(tmp, StringComparison.Ordinal) != -1)
                    {
                        delimiter = tmp;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(delimiter))
            {
                date += delimiter;

                int i = 0, j = 0, k = 0;
                while ((i = date.IndexOf(delimiter, i, StringComparison.Ordinal)) != -1)
                {
                    string s = date.Substring(j, i - j);
                    dateVar[k] = s;
                    j = i + 1; i++; k++;
                }
            }
            else
            {
                if (date.Length != 8)
                    return retValue;

                dateVar[2] = date.Substring(0, 4);
                dateVar[1] = date.Substring(4, 2);
                dateVar[0] = date.Substring(6, 2);
            }

            dateVar[2] = dateVar[2].Trim();
            dateVar[1] = dateVar[1].Trim();
            dateVar[0] = dateVar[0].Trim();

            if (!IsNumeric(dateVar[0]) || !IsNumeric(dateVar[1]) || !IsNumeric(dateVar[2]))
                return retValue;

            if (dateVar[2].Length == 2)
                dateVar[2] = "20" + dateVar[2];

            DD = Convert.ToInt32(dateVar[0]);
            MM = Convert.ToInt32(dateVar[1]);
            YY = Convert.ToInt32(dateVar[2]);

            if ((DD == 0) || (MM == 0) || (YY == 0))
                return retValue;

            int calType = 0;
            if (YY >= baseStart && YY <= baseEnd)
                calType = 1;

            if (calType == 0)
            {
                if (YY >= localStart && YY <= localEnd)
                    calType = 2;
            }

            if (calType == 0)
                return retValue;

            Calendar calender;
            if (calType == 2)
                calender = localCalendar;
            else
                calender = baseCalendar;

            int Dys = 0;
            var mths = calender.GetMonthsInYear(YY);
            if (MM <= mths)
                Dys = calender.GetDaysInMonth(YY, MM);

            return Dys;
        }

        public string ConvertToLocalCalendar(string date)
        {
            string formattedDate = FormatDateString(date);

            if (string.IsNullOrEmpty(formattedDate))
                return string.Empty;

            string sdt = formattedDate.Substring(6, 4) + "-" + formattedDate.Substring(3, 2) + "-" + formattedDate.Substring(0, 2);
            var dateOk = DateTime.TryParse(sdt, out var dt);

            if (!dateOk)
                return formattedDate;

            if (dt.Year >= localStart && dt.Year <= localEnd)
                return formattedDate;

            string DateLangCulture = localCultureCode.ToLower();

            var DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;
            DTFormat.Calendar = new System.Globalization.HijriCalendar();

            DTFormat.ShortDatePattern = "dd/MM/yyyy";

            string dtString = (dt.ToString("f", DTFormat));
            return dtString.Substring(0, 10);

        }


        public string ConvertToBaseCalendar(string date)
        {
            string formattedDate = FormatDateString(date);

            if (string.IsNullOrEmpty(formattedDate))
                return string.Empty;

            Int32 year = Convert.ToInt32(formattedDate.Substring(6, 4));
            if (year >= baseStart && year <= baseEnd)
                return formattedDate;

            try
            {
                return DateTime.ParseExact(formattedDate, "dd/MM/yyyy", new CultureInfo(localCultureCode)).ToString("dd/MM/yyyy", new CultureInfo(baseCultureCode));
            }

            catch (Exception)
            {
                try
                {
                    return DateTime.ParseExact(formattedDate, "dd/MM/yyyy", localCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces).ToString("dd/MM/yyyy", baseCulture.DateTimeFormat);
                }
                catch (Exception)
                {
                    //  curContext.Trace.Warn("Date conversion Error: " + exc.GetType() + ",  Message: " + exc.Message);
                    return formattedDate;
                }
            }
        }


        public string AddDaysToDate(string date, int addDays)
        {
            string formattedDate = FormatDateString(date);

            if (string.IsNullOrEmpty(formattedDate))
                return string.Empty;

            int D = Convert.ToInt32(formattedDate.Substring(0, 2));
            int M = Convert.ToInt32(formattedDate.Substring(3, 2));
            int Y = Convert.ToInt32(formattedDate.Substring(6, 4));

            DateTime DTM = new DateTime(Y, M, D);
            DateTime NewDTM = DTM.AddDays(addDays);

            return NewDTM.Day.ToString().PadLeft(2, '0') + "/" + NewDTM.Month.ToString().PadLeft(2, '0') + "/" + NewDTM.Year.ToString().PadLeft(4, '0');
        }

        public string AddMonthsToDate(string date, int months)
        {
            string formattedDate = FormatDateString(date);

            if (string.IsNullOrEmpty(formattedDate))
                return string.Empty;

            int D = Convert.ToInt32(formattedDate.Substring(0, 2));
            int M = Convert.ToInt32(formattedDate.Substring(3, 2));
            int Y = Convert.ToInt32(formattedDate.Substring(6, 4));

            DateTime DTM = new DateTime(Y, M, D);
            DateTime NewDTM = DTM.AddMonths(months);

            return NewDTM.Day.ToString().PadLeft(2, '0') + "/" + NewDTM.Month.ToString().PadLeft(2, '0') + "/" + NewDTM.Year.ToString().PadLeft(4, '0');
        }

        private bool IsNumeric(string s)
        {
            bool match;
            //regular expression to match numeric values
            //string pattern = "(^[-+]?\\d+(,?\\d*)*\\.?\\d*([Ee][-+]\\d*)?$)|(^[-+]?\\d?(,?\\d*)*\\.\\d+([Ee][-+]\\d*)?$)";
            string pattern = "(^[0-9]*$)";
            Regex regEx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            match = regEx.Match(s).Success;
            return match;
        }


        /// <summary>
        /// Returns empty when the date is valid, else returns the validation failure message
        /// </summary>
        /// <param name="date"></param>
        /// <param name="fsStartDate"></param>
        /// <param name="fsEndDate"></param>
        /// <param name="fsPostedUptoDate"></param>
        /// <param name="vType"></param>
        /// <returns></returns>
        public string ValidateDate(string date, string fsStartDate, string fsEndDate, string fsPostedUptoDate, DateValidationTypes vType = DateValidationTypes.NoCheck)
        {
            int fsStartDateInteger = 0;
            int fsEndDateInteger = 0;
            int fsPostedUptoDateInteger = 0;
            try
            {
                if (!(string.IsNullOrEmpty(fsStartDate)))
                    fsStartDateInteger = int.Parse(Date_SaveFormat(fsStartDate));

                if (!(string.IsNullOrEmpty(fsEndDate)))
                    fsEndDateInteger = int.Parse(Date_SaveFormat(fsEndDate));

                if (!(string.IsNullOrEmpty(fsPostedUptoDate)))
                    fsPostedUptoDateInteger = int.Parse(Date_SaveFormat(fsPostedUptoDate));
            }
            catch (Exception)
            {
                return string.Empty;
            }

            //1 only check for format

            if (vType == DateValidationTypes.NoCheck)
                return string.Empty;

            string sdate = Date_SaveFormat(date);

            if (string.IsNullOrEmpty(sdate))
                return "1052";

            if (vType == DateValidationTypes.FormatCheck)
                return string.Empty;

            int sDateInteger = int.Parse(sdate);

            if (sDateInteger < fsStartDateInteger)
                return "1054";

            if (vType == DateValidationTypes.StartYearCheck)
                return string.Empty;

            if (sDateInteger > fsEndDateInteger)
                return "1055";

            if (vType == DateValidationTypes.EndYearCheck)
                return string.Empty;

            if (sDateInteger <= fsPostedUptoDateInteger)
                return "3686"; //"1056";

            if (vType == DateValidationTypes.FullCheck)
                return string.Empty;

            return string.Empty;
        }


        public string StartingEndingDatesOfMonth(string yearMonth)
        {
            string result = string.Empty;

            if (yearMonth.Length != 6)
                return result;

            string lStart = yearMonth + "01";
            int nDays = GetTotalDaysInMonth(lStart);
            //result = lStart + ", " + yearMonth + nDays.ToString();

            string lEnd = yearMonth + nDays.ToString();
            result = Date_SaveFormat(lStart) + "," + Date_SaveFormat(lEnd);

            return result;
        }



        /////////////////////////////////////////////////////////

        private string MakeDecimalFormat(int nd)
        {
            string ret = string.Empty;
            for (int i = 0; i < nd; i++)
                ret += "0";
            return "." + ret;
        }

        public string Amount_DisplayFormat(decimal amount, int decimalsInAmount, int profileAmountFormat)
        {
            if (amount == 0)
                return "0" + MakeDecimalFormat(decimalsInAmount);

            string integerPart = InsertCommas_Format(amount, decimalsInAmount, profileAmountFormat);

            if (decimalsInAmount == 0)
                return integerPart;

            string ret = amount.ToString(MakeDecimalFormat(decimalsInAmount));

            string integerPart2 = InsertCommas_Format(Convert.ToDecimal(ret), decimalsInAmount, profileAmountFormat);

            if (integerPart != integerPart2)
            {
                integerPart = integerPart2;
            }

            string decimalPart = ret.Substring(ret.Length - decimalsInAmount, decimalsInAmount);
            if (integerPart == "")
            {
                integerPart = "0";
            }
            return integerPart + "." + decimalPart;
        }

        public string Quantity_DisplayFormat(decimal quantity, int decimalsInQuantity)
        {
            if (quantity == 0)
                return "0" + MakeDecimalFormat(decimalsInQuantity);

            string integerPart;

            if (decimalsInQuantity == 0)
            {
                integerPart = Convert.ToInt64(quantity).ToString();
                return integerPart;
            }
            else
            {
                string x = quantity.ToString(".000000");
                integerPart = x.Substring(0, x.Length - 7);
            }

            string ret = quantity.ToString(MakeDecimalFormat(decimalsInQuantity));

            string decimalPart = ret.Substring(ret.Length - decimalsInQuantity, decimalsInQuantity);
            if (integerPart == "")
            {
                integerPart = "0";
            }
            return integerPart + "." + decimalPart;
        }


        public string Price_DisplayFormat(decimal price, int decimalsInPrice, int profileAmountFormat)
        {
            return Amount_DisplayFormat(price, decimalsInPrice, profileAmountFormat);
        }

        public string Currency_DisplayFormat(decimal currencyRate, int decimalsInCurrencyRate = 4)
        {
            return Amount_DisplayFormat(currencyRate, decimalsInCurrencyRate, 0);
        }

        private string InsertCommas_Format(decimal amount, int numberOfDecimals, int profileAmountFormat)
        {
            string str;
            bool isNegativeValue = false;
            if (numberOfDecimals == 0)
                str = Convert.ToInt64(amount).ToString();
            else
            {
                string x = amount.ToString(".000000");
                str = x.Substring(0, x.Length - 7);
                if (str.Contains('-'))
                {
                    str = str.Replace("-", string.Empty);
                    isNegativeValue = true;
                }
            }

            if ((str.Length <= 3) || (profileAmountFormat == 1))
            {
                if (isNegativeValue)
                    return string.Concat("-", str);
                return str;
            }

            string retValue = str.Substring(str.Length - 3, 3);


            string fmtExpr = (profileAmountFormat == 2 ? "(?<=\\d{1})(?=(\\d{2})+(?!\\d{1}))" : "(?<=\\d{1})(?=(\\d{3})+(?!\\d{1}))");

            Regex r1 = new Regex(fmtExpr);

            retValue = r1.Replace(str.Substring(0, str.Length - 3), ",") + "," + retValue;
            if (isNegativeValue)
                return string.Concat("-", retValue);
            return retValue;
        }


        public DataTable General_CombineColumns(DataTable dt, string col1Name, string col2Name, string combinedName, string separator)
        {
            DataTable ret = dt.Clone();

            ret.Columns.Add(new DataColumn(combinedName, typeof(string)));

            foreach (DataRow dr in dt.Rows)
                ret.ImportRow(dr);


            foreach (DataRow dr in ret.Rows)
            {
                dr[combinedName] = dr[col1Name] + separator + dr[col2Name];
            }

            return ret;
        }
        public DateTime StringParseToDate(string _Date)
        {

            string Date = Date_DisplayFormatInEng(_Date);
            string[] words = Date.Split('/');
            int day = Convert.ToInt32(words[0]);
            int Month = Convert.ToInt32(words[1]);
            int Year = Convert.ToInt32(words[2]);
            CultureInfo culture = new CultureInfo("en-US");
            DateTime dt = Convert.ToDateTime("" + Month + "/" + day + "/" + Year + "", culture);
            return dt;
        }
        public string Date_DisplayFormatInEng(string date)
        {
            string showDt = FormatDateString(date);

            return showDt;
        }
    }
}