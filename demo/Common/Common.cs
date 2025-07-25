using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace demo.Common
{
    public class Common
    {
        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            decimal GT = 0;

            // Nếu giá trị là kiểu string, thử chuyển đổi sang decimal
            if (value is string stringValue)
            {
                if (decimal.TryParse(stringValue, out decimal parsedValue))
                {
                    GT = parsedValue;
                }
            }
            // Nếu giá trị là số
            else if (IsNumeric(value))
            {
                GT = Convert.ToDecimal(value);
            }

            string str = "";
            string thapPhan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapPhan += "#";
            }

            if (thapPhan.Length > 0)
            {
                thapPhan = "." + thapPhan;
            }

            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);

            return str;
        }

        public static bool IsNumeric(object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
        }
    }
}