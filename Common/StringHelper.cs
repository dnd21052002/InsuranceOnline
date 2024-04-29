using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public static class StringHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(":", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        public static string FormatCurrency(decimal value)
        {
            CultureInfo culture = new CultureInfo("vi-VN"); // Sử dụng ngôn ngữ và vùng miền Việt Nam
            var a = value.ToString("C0", culture).Replace(" ", ""); // "C0" định dạng số tiền
            return a;
        }

        public static string FormatDiscountPercentage(decimal discountPercentage)
        {
            int roundedPercentage = (int)Math.Floor(discountPercentage);
            return roundedPercentage.ToString() + "%";
        }
    }
}