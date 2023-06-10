using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace webapi.Extensions
{
    public static class StringExtensions
    {
        public static string ToSQLLike(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : $"%{value}%";
        }

        public static string Interpolate(this string template, IDictionary<string, string> data)
        {
            if (data == null)
            {
                return template;
            }

            var regex = new Regex(@"\[([^\]]+)\]");
            return regex.Replace(template, match =>
            {
                var key = match.Groups[1].ToString();
                if (!data.ContainsKey(key))
                {
                    return match.ToString();
                }

                return data[key]?.ToString();
            });
        }

        public static string NA_IfNullOrEmpty(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "N/A";
            }
            return value;
        }

        public static string RemoveDot(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return text.Replace(".", string.Empty);
        }

        public static string SafeString(this object text)
        {
            if (text == null)
            {
                return "";
            }

            return text.ToString().Trim();
        }

        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsHasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Cleans a string of harmful chars
        /// </summary>
        /// <param name="value">the dirty string</param>
        /// <returns>the cleaned string</returns>
        public static string CleanString(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value.Trim();
            return null;
        }

        public static string ToLikeSearch(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return $"%{value.Trim()}%";
            return null;
        }

        public static int? ToInt(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            var valid = int.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out int temp);
            return valid ? temp : (int?)null;
        }

        public static decimal? ToDecimal(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            var valid = decimal.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out decimal temp);
            return valid ? temp : (decimal?)null;
        }

        public static double? ToDouble(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            var valid = double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out double temp);
            return valid ? temp : (double?)null;
        }

        public static double ToDoubleZero(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return 0;

            var valid = double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out double temp);
            return valid ? temp : 0;
        }

        public static double? ToDouble(this decimal? value)
        {
            if (value == null)
                return null;

            return (double)value;
        }

        public static double ToDoubleZero(this decimal? value)
        {
            if (value == null)
                return 0;

            return (double)value;
        }

        public static float? ToFloat(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            var valid = float.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out float temp);
            return valid ? temp : (float?)null;
        }

        public static long? ToLong(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            var valid = long.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out long temp);
            return valid ? temp : (long?)null;
        }

        public static long ToLongNotNull(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return 0;
            var valid = long.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out long temp);
            return valid ? temp : 0;
        }

        public static bool IsNullOrEmpty(this List<string> values)
        {
            return values.Any(x => string.IsNullOrEmpty(x));
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static Guid ParseGuid(this string value)
        {
            var valid = Guid.TryParse(value, out Guid temp);
            return valid ? temp : Guid.Empty;
        }
        public static Guid ToGuid(this string value)
        {
            var valid = Guid.TryParse(value, out Guid temp);
            return valid ? temp : Guid.Empty;
        }

        public static byte[] ToByteArray(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static string ToStringASCII(this byte[] value)
        {
            return Encoding.ASCII.GetString(value);
        }

        //public static string GetQRCode(this string value)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    var qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);
        //    return "data:image/png;base64," + Convert.ToBase64String(BitmapToBytes(qrCodeImage));
        //}

        public static string GetDataMatrix(this string value)
        {
            return "";

        }
        //private static Byte[] BitmapToBytes(Bitmap img)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}

        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool NotContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            if (values.Count() == 0)
                return true;

            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
