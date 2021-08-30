/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;

namespace ccoftBLL.SYSTEM
{
    public static class Helper
    {
        static public string clearTurkishChar(this string p_sText)
        {
            string l_sResult = "";
            l_sResult = p_sText.Replace("ğ", "g");
            l_sResult = l_sResult.Replace("Ğ", "G");
            l_sResult = l_sResult.Replace("ü", "u");
            l_sResult = l_sResult.Replace("Ü", "U");
            l_sResult = l_sResult.Replace("ş", "s");
            l_sResult = l_sResult.Replace("Ş", "S");
            l_sResult = l_sResult.Replace("ı", "i");
            l_sResult = l_sResult.Replace("İ", "I");
            l_sResult = l_sResult.Replace("ö", "o");
            l_sResult = l_sResult.Replace("Ö", "O");
            l_sResult = l_sResult.Replace("ç", "c");
            l_sResult = l_sResult.Replace("Ç", "C");
            return l_sResult;
        }
        static public string clearUnicode(this string p_sText)
        {
            string l_sResult = p_sText;
            l_sResult = l_sResult.Replace("&#305;", "ı");
            l_sResult = l_sResult.Replace("&#231;", "ç");
            l_sResult = l_sResult.Replace("&#351;", "ş");
            l_sResult = l_sResult.Replace("&#246;", "ö");
            l_sResult = l_sResult.Replace("&#252;", "ü");
            l_sResult = l_sResult.Replace("&#287;", "ğ");
            l_sResult = l_sResult.Replace("&#304;", "İ");
            l_sResult = l_sResult.Replace("&#199;", "Ç");
            l_sResult = l_sResult.Replace("&#350;", "Ş");
            l_sResult = l_sResult.Replace("&#214;", "Ö");
            l_sResult = l_sResult.Replace("&#220;", "Ü");
            l_sResult = l_sResult.Replace("&#286;", "Ğ");
            l_sResult = l_sResult.Replace("&#160;", " ");
            return l_sResult;
        }
        static public string getApostrophe(this string p_sValue)
        {
            string l_sResult = p_sValue.Replace("'", "''");
            return l_sResult;
        }
        static public string getString(this DataRow p_cDataRow, string p_sColumnName)
        {
            string l_sResult = "";
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                        l_sResult = Convert.ToString(p_cDataRow[p_sColumnName]);
            }
            return l_sResult;
        }
        static public int getInt(this DataRow p_cDataRow, string p_sColumnName)
        {
            int l_sResult = 0;
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                        l_sResult = Convert.ToInt32(p_cDataRow[p_sColumnName]);
            }
            return l_sResult;
        }
        static public TimeSpan getTimeSpan(this DataRow p_cDataRow, string p_sColumnName)
        {
            TimeSpan l_sResult = new TimeSpan();
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                    {
                        l_sResult = TimeSpan.Parse(p_cDataRow[p_sColumnName].ToString());
                    }
            }
            return l_sResult;
        }
        static public double getDouble(this DataRow p_cDataRow, string p_sColumnName)
        {
            double l_sResult = 0;
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                        l_sResult = Convert.ToDouble(p_cDataRow[p_sColumnName]);
            }
            return l_sResult;
        }
        static public DateTime getDate(this DataRow p_cDataRow, string p_sColumnName)
        {
            DateTime l_sResult = DateTime.Now;
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                        l_sResult = Convert.ToDateTime(p_cDataRow[p_sColumnName]);
            }
            return l_sResult;
        }
        static public Boolean getBool(this DataRow p_cDataRow, string p_sColumnName)
        {
            Boolean l_sResult = false;
            if (p_cDataRow.Table.Columns.Contains(p_sColumnName))
            {
                if (p_cDataRow[p_sColumnName] != DBNull.Value)
                    if (p_cDataRow[p_sColumnName] != null)
                        l_sResult = Convert.ToBoolean(p_cDataRow[p_sColumnName]);
            }
            return l_sResult;
        }
        static public bool IsOk(this int p_sValue)
        {
            return (p_sValue == 0 || p_sValue == 1);
        }
        static public bool IsId(this int p_sValue)
        {
            return p_sValue > 0;
        }
        static public bool IsOk(this string p_sValue)
        {
            return !string.IsNullOrEmpty(p_sValue);
        }
        static public bool IsPositive(this double p_dValue)
        {
            return p_dValue >= 0;
        }
        static public string ClearPhoneNumber(this string p_sValue)
        {
            return p_sValue.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        }
        static public string ToDateSql(this string p_sDatetime)
        {
            string l_sResult = "CONVERT(date, '" + p_sDatetime + "',104)";
            try
            {

                if (p_sDatetime.Contains(':'))
                {
                    string[] l_sMainResult = p_sDatetime.Split(' ');
                    string[] l_sDateResult = l_sMainResult[0].Split('.');
                    l_sResult = l_sDateResult[2] + "-" + l_sDateResult[1] + "-" + l_sDateResult[0] + " " + l_sMainResult[1];
                    l_sResult = "CONVERT(datetime, '" + l_sResult + "',121)";
                }
            }
            catch
            {
                return l_sResult;
            }
            return l_sResult;
        }
        static public string GetDateTimeNow(this DateTime p_dtDatetime)
        {
            DateTime l_dtNow = p_dtDatetime;
            string l_sResult = "" + l_dtNow.Day + "." + l_dtNow.Month + "." + l_dtNow.Year + " " + l_dtNow.Hour + ":" + l_dtNow.Minute + ":" + l_dtNow.Second + "." + l_dtNow.Millisecond;
            return l_sResult;
        }
        static public string GetDateTimeAsDateAndTime(this DateTime p_dtDatetime)
        {
            DateTime l_dtNow = p_dtDatetime;
            string l_sResult = "" + l_dtNow.Day + "." + l_dtNow.Month + "." + l_dtNow.Year + " " + l_dtNow.Hour + ":" + l_dtNow.Minute;
            return l_sResult;
        }
        static public DateTime ToDate(this string p_sDatetime)
        {
            DateTime l_cDateTime = DateTime.Now;
            try
            {

                if (p_sDatetime.Contains(':'))
                {
                    string[] l_sMainResult = p_sDatetime.Split(' ');
                    string[] l_sDateResult = l_sMainResult[0].Split('.');
                    l_cDateTime = Convert.ToDateTime(l_sDateResult[2] + "-" + l_sDateResult[1] + "-" + l_sDateResult[0] + " " + l_sMainResult[1]);
                }
                else
                {
                    string[] l_sDateResult = p_sDatetime.Split('.');
                    l_cDateTime = Convert.ToDateTime(l_sDateResult[2] + "-" + l_sDateResult[1] + "-" + l_sDateResult[0]);
                }
            }
            catch
            {
                return l_cDateTime;
            }
            return l_cDateTime;
        }
        static public bool IsPassenger(this System.Security.Principal.IPrincipal p_User)
        {
            bool l_isResult = false;
            if (p_User.Identity.AuthenticationType == "OK" || p_User.Identity.AuthenticationType == "PASSENGER")
                l_isResult = true;
            return l_isResult;
        }
        static public bool IsSystemUser(this System.Security.Principal.IPrincipal p_User)
        {
            bool l_isResult = false;
            if (p_User.Identity.AuthenticationType == "OK" || p_User.Identity.AuthenticationType == "SYSTEM")
                l_isResult = true;
            return l_isResult;
        }
        static public int ToInt32(this string param)
        {
            if (param.IsNullOrEmpty())
                return 0;
            int i = 0;
            if (int.TryParse(param, out i))
                return Convert.ToInt32(param);

            return 0;
        }
        static public bool ToBoolean(this string param)
        {
            if (param.IsNullOrEmpty())
                return false;

            return Convert.ToBoolean(param);
        }
        static public DateTime ToDateTime(this string param)
        {
            return Convert.ToDateTime(param);
        }
        static public decimal ToDecimal(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return 0;
            return Convert.ToDecimal(param);
        }
        static public double ToDouble(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return 0;
            return Convert.ToDouble(param);
        }
        static public float ToFloat(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return 0;
            return (float)Convert.ToDouble(param);
        }
        static public bool IsNullOrEmpty(this string param)
        {
            if (param == null)
                return true;
            if (string.IsNullOrEmpty(param))
                return true;
            return false;
        }
        static public int ToInt32(this object param)
        {
            if (param == DBNull.Value)
                return 0;
            return Convert.ToInt32(param);
        }
        static public DateTime ToDateTime(this object param)
        {
            return Convert.ToDateTime(param);
        }
        static public bool ToBoolean(this object param)
        {
            if (param == DBNull.Value)
                return false;
            return Convert.ToBoolean(param);
        }
        static public decimal ToDecimal(this object param)
        {
            if (string.IsNullOrEmpty(param.ToString()))
                return 0;
            return Convert.ToDecimal(param);
        }
        static public double ToDouble(this object param)
        {
            if (string.IsNullOrEmpty(param.ToString()))
                return 0;
            return Convert.ToDouble(param);
        }
        static public float ToFloat(this object param)
        {
            if (string.IsNullOrEmpty(param.ToString()))
                return 0;
            return (float)Convert.ToDouble(param);
        }
        static public bool TryPasreInt(this string param)
        {
            int i = 0;
            if (int.TryParse(param, out i))
                return true;

            return false;
        }
        static public bool TryPasreLong(this string param)
        {
            long i = 0;
            if (long.TryParse(param, out i))
                return true;

            return false;
        }
        public static bool isOk(string p_sParameter)
        {
            bool l_bIsOk = true;
            List<string> l_sStringList = new List<string>();
            l_sStringList.Add("droptable");
            l_sStringList.Add("select");
            l_sStringList.Add("delete");
            l_sStringList.Add("update");
            for (int i = 0; i < l_sStringList.Count; i++)
            {
                if (p_sParameter.Trim().ToLower().Contains(l_sStringList[i]))
                {
                    l_bIsOk = false;
                }
            }
            return l_bIsOk;
        }
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        public static double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (Math.Round(dist, 1));
            }
        }
    }
}
