/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ValidatorExtensions Description:
 * ��֤��չ
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ͨ�ð���
    /// </summary>
    public partial class CommonHelper
    {
        #region Fields

        //we use EmailValidator from FluentValidation. So let's keep them sync - https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/Validators/EmailValidator.cs
        private const string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

        private static readonly Regex _emailRegex;

        #endregion

        #region Ctor
        /// <summary>
        /// ctor
        /// </summary>
        static CommonHelper()
        {
            _emailRegex = new Regex(_emailExpression, RegexOptions.IgnoreCase);
        }

        #endregion

        #region Methods
        /// <summary>
        /// �����µ�Guid
        /// </summary>
        /// <param name="removeLine"></param>
        /// <returns></returns>
        public static string NewGuid(bool removeLine = true)
        {
            if (removeLine)
            {
                // ȥ�����ת���ɴ�д
                return Guid.NewGuid().ToString("N").ToUpper();
            }
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Ĭ��Guidȥ��-
        /// </summary>
        public static string EmptyGuid
        {
            get => Guid.Empty.ToString("N");
        }

        /// <summary>
        /// ��֤�ַ����Ƿ�Ϊ��Ч�ĵ����ʼ���ʽ
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();

           return _emailRegex.IsMatch(email);
        }

        /// <summary>
        /// ��֤ip��ַ
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out IPAddress _);
        }

        /// <summary>
        /// ����������ִ���
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// ����ָ����Χ�ڵ��������
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// ȷ���ַ����������������󳤶�
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its length is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length <= maxLength) 
                return str;

            var pLen = postfix?.Length ?? 0;

            var result = str.Substring(0, maxLength - pLen);
            if (!string.IsNullOrEmpty(postfix))
            {
                result += postfix;
            }

            return result;
        }

        /// <summary>
        /// ȷ���ַ�����Ϊnull
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// ָʾָ�����ַ����ǿ��ַ�������null
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            return stringsToValidate.Any(string.IsNullOrEmpty);
        }

        /// <summary>
        /// �Ƚ���������
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
        }
        
        /// <summary>
        /// �趨����ֵ
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new NGPException(StatusCode.EmptyError, nameof(instance));
            if (propertyName == null) throw new NGPException(StatusCode.EmptyError, nameof(propertyName));

            var instanceType = instance.GetType();
            var pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new NGPException(StatusCode.TypeNotPropertyError, propertyName, instanceType.Name);
            if (!pi.CanWrite)
                throw new NGPException(StatusCode.TypePropertyNotSetError, propertyName, instanceType.Name);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value == null || value is DBNull) 
                return null;

            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);

            return value;
        }

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);

            var res = To(value, typeof(T));
            if (res == null)
                return default(T);
            return (T)To(value, typeof(T));
        }

        #region �ַ���ת����ö��
        /// <summary>
        /// �ַ���ת����ö��
        /// </summary>
        /// <typeparam name="TEnum">ö������</typeparam>
        /// <param name="value">�ַ���ֵ</param>
        /// <param name="defaultValue">ö��Ĭ��ֵ</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(string value, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            Enum.TryParse(value, true, out defaultValue);
            return defaultValue;
        }
        #endregion

        /// <summary>
        /// ��ö��ת��Ϊstring
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            var result = string.Empty;
            foreach (var c in str)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();

            //ensure no spaces (e.g. when the first letter is upper case)
            result = result.TrimStart();
            return result;
        }

        /// <summary>
        /// ��ȡ˽���ֶ�����ֵ
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Value</returns>
        public static object GetPrivateFieldValue(object target, string fieldName)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target", "The assignment target cannot be null.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("fieldName", "The field name cannot be null or empty.");
            }

            var t = target.GetType();
            FieldInfo fi = null;

            while (t != null)
            {
                fi = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

                if (fi != null) break;

                t = t.BaseType;
            }

            if (fi == null)
            {
                throw new Exception($"Field '{fieldName}' not found in type hierarchy.");
            }

            return fi.GetValue(target);
        }

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string Encrypt(string src)
        {
            bool flag = src == "";
            string result;
            if (flag)
            {
                result = src;
            }
            else
            {
                Encoding unicode = Encoding.Unicode;
                byte[] bytes = unicode.GetBytes(src);
                byte[] inArray = Encrypt(bytes);
                string text = Convert.ToBase64String(inArray);
                result = text;
            }
            return result;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] input)
        {
            byte[] result;
            using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                ICryptoTransform cryptoTransform = dESCryptoServiceProvider.CreateEncryptor(_rgbKey, _rgbIV);
                byte[] array = cryptoTransform.TransformFinalBlock(input, 0, input.Length);
                result = array;
            }
            return result;
        }
        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string Decrypt(string src)
        {
            bool flag = src == "";
            string result;
            if (flag)
            {
                result = src;
            }
            else
            {
                Encoding unicode = Encoding.Unicode;
                byte[] input = Convert.FromBase64String(src);
                byte[] bytes = Decrypt(input);
                string @string = unicode.GetString(bytes);
                result = @string;
            }
            return result;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] input)
        {
            byte[] result;
            using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                ICryptoTransform cryptoTransform = dESCryptoServiceProvider.CreateDecryptor(_rgbKey, _rgbIV);
                byte[] array = cryptoTransform.TransformFinalBlock(input, 0, input.Length);
                result = array;
            }
            return result;
        }
        #endregion
        /// <summary>
        /// rgb key
        /// </summary>
        private static readonly byte[] _rgbKey = new byte[]
            {
                19,
                144,
                17,
                153,
                147,
                19,
                128,
                18
            };
        /// <summary>
        /// rgb iv
        /// </summary>
        private static readonly byte[] _rgbIV = new byte[]
            {
                8,
                1,
                65,
                57,
                1,
                25,
                153,
                49
            };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default file provider
        /// </summary>
        public static INGPFileProvider DefaultFileProvider { get; set; }

        #endregion
    }
}
