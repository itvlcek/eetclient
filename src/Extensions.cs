using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace EETLib
{
    public static class Extensions
    {
        /// <summary>
        /// Convert date to required format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetAsText(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssK");
        }

        /// <summary>
        /// Get money as text
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMoneyAsTextUSFormat(this decimal value)
        {
            // en-US 0.00 format
            return value.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectData"></param>
        /// <returns></returns>
        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        /// <summary>
        /// Deserialize object from string by type
        /// </summary>
        /// <param name="objectData"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
