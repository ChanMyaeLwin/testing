using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigoPapaerWorkProcessSystem.Core
{
    public static class CheckUtility
    {
        #region DateFormatCheck
        public static bool DateFormatCheck(string value)
        {
            bool pass = true;

            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    pass = false;
                }
            }
            return pass;
        }

        public static bool YearCheck(string value)
        {
            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool YearMonthCheck(string value)
        {

            DateTime Date;
            try
            {
                Date = DateTime.ParseExact(value, "yyyy/MM", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return true;
            }
            catch (Exception)
            {
                try
                {
                    Date = DateTime.ParseExact(value, "yyyy/M", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        #endregion

        #region RationalDateCheck
        public static bool DateRationalCheck(String fromDate, String toDate)
        {
            DateTime? from = null;
            DateTime? to = null;

            string value1 = fromDate.Length == 4 ? fromDate + "/01/01" : fromDate;
            string value2 = toDate.Length == 4 ? toDate + "/01/01" : toDate;
            try
            {
                from = DateTime.Parse(value1);
            }
            catch (Exception)
            {
            }

            try
            {
                to = DateTime.Parse(value2);
            }
            catch (Exception)
            {

            }            
            
            //check if todate is greater than fromdate
            if (from <= to)
            {
                if (to == null)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (from == null && to !=null) //check if only to date is inserted 
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
