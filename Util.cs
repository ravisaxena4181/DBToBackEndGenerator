using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDBToBackEndGenerator
{
    public static class Util
    {
        internal static string GetConnection()
        {
            throw new NotImplementedException();
        }
      
        internal static double ToDouble(object val)
        {
            throw new NotImplementedException();
        }
        public static int ToInt32(string p)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                    return 0;
                else
                    return Convert.ToInt32(p);
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
        public static DateTime ToDate(object p)
        {
            try
            {
                if (string.IsNullOrEmpty(p.ToString()))
                    return DateTime.MinValue;
                else
                    return Convert.ToDateTime(p);
            }
            catch (Exception ex)
            {

                return DateTime.MinValue; ;
            }
        }

        public static int ToInt32(object p)
        {
            try
            {
                if (string.IsNullOrEmpty(p.ToString()))
                    return 0;
                else
                    return Convert.ToInt32(p);
            }
            catch (Exception ex)
            {

                return 0;
            }


        }
       
        public static bool ToBoolean(object p)
        {
            try
            {
                return Convert.ToBoolean(p);
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static bool ToBoolean(string p)
        {
            try
            {
                return Convert.ToBoolean(p);
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static decimal ToDecimal(string p)
        {
            try
            {
                return Convert.ToDecimal(p);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static decimal ToDecimal(object p)
        {
            try
            {
                return Convert.ToDecimal(p);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
