using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Utility
{
    public static class GeneralFunctions
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static DateTime ToDateTime(this string datetime, char dateSpliter = '-', char timeSpliter = ':', char millisecondSpliter = ',')
        {
            try
            {
                datetime = datetime.Trim();
                datetime = datetime.Replace("  ", " ");
                string[] body = datetime.Split(' ');
                string[] date = body[0].Split(dateSpliter);
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                int hour = 0, minute = 0, second = 0, millisecond = 0;
                if (body.Length == 2)
                {
                    string[] tpart = body[1].Split(millisecondSpliter);
                    string[] time = tpart[0].Split(timeSpliter);
                    hour = int.Parse(time[0]);
                    minute = int.Parse(time[1]);
                    if (time.Length == 3) second = int.Parse(time[2]);
                    if (tpart.Length == 2) millisecond = int.Parse(tpart[1]);
                }
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch
            {
                return new DateTime();
            }
        }
    }
}
