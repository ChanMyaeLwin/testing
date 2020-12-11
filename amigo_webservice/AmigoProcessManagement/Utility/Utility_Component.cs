using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;

namespace AmigoProcessManagement.Utility
{
    public class Utility_Component
    {
        #region dtColumnToDateTime
        public static DateTime? dtColumnToDateTime(string strValue)
        {
            DateTime dtm = new DateTime();
            DateTime.TryParse(strValue, out dtm);
            if (dtm == new DateTime())
            {
                return null;
            }
            return dtm;
        }
        #endregion

        #region dtColumnToDecimal
        public static decimal dtColumnToDecimal(string strValue)
        {
            Decimal decTMP = 0;
            Decimal.TryParse(strValue, out decTMP);
            return decTMP;
        }
        #endregion        

        #region dtColumnToInt
        public static int dtColumnToInt(string strValue)
        {
            int intTMP = 0;
            int.TryParse(strValue, out intTMP);
            return intTMP;
        }
        #endregion

        #region DtToJSon
        public static string DtToJSon(DataTable dt, string strHeader)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
                    dictionary.Add(column.ColumnName, row[column]);
                dictionaryList.Add(dictionary);
            }
            return "{ \"" + strHeader + "\" : " + scriptSerializer.Serialize((object)dictionaryList) + "}";
        }
        #endregion

        #region JsonToDt
        public static DataTable JsonToDt(string strJSON)
        {
            DataSet dataSet = (DataSet)JsonConvert.DeserializeObject<DataSet>(strJSON);
            DataTable dt = new DataTable();
            if (dataSet.Tables.Count > 0)
            {
                dt = dataSet.Tables[0];
            }
            return dt;
        }
        #endregion

        #region GetDate
        public static string GetYearMonth(string date, bool from)
        {
            if (!string.IsNullOrEmpty(date))
            {
                if (date.Length == 4)
                {
                    if (from)
                    {
                        return date.Substring(2).PadRight(4, '0');
                    }
                    else
                    {
                        return date.Substring(2) + "12";
                    }

                }
                else if (date.Length > 4)
                {
                    return DateTime.Parse(date).ToString("yyMM");
                }
            }
            return date;
        }

        public static string GetFullDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return DateTime.Parse(date).ToString("yyyyMMdd");
            }
            return date;
        }
        #endregion

    }
}