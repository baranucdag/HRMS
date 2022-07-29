using IdentityServer4.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using XSystem.Security.Cryptography;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        // variables
        public static string TrChars = "ğüşıöçĞÜŞİÖÇ";
        public static string CompanyKey = "ARKSOFT";

        // IsNullOrEmpty - boş mu değil mi
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        // IsNullOrEmpty - boş mu değil mi
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }


        // ToUpperEng - stringi ingilizce büyük harfe çevirir
        public static string ToUpperEng(this string str)
        {
            return str.ToUpper(true);
        }

        // ToUpper - stringi büyük harfe çevirir
        public static string ToUpper(this string str, bool useEnglish = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (str.Length == 0)
            {
                return string.Empty;
            }
            List<string> pattern = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-" };

            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
            {

                if (useEnglish)
                {
                    switch (c.ToString())
                    {
                        case "i":
                        case "ı":
                        case "İ":
                            {
                                sb.Append("I");
                                break;
                            }
                        case "ğ":
                        case "Ğ":
                            {
                                sb.Append("G");
                                break;
                            }
                        case "ü":
                        case "Ü":
                            {
                                sb.Append("U");
                                break;
                            }
                        case "ş":
                        case "Ş":
                            {
                                sb.Append("S");
                                break;
                            }
                        case "ö":
                        case "Ö":
                            {
                                sb.Append("O");
                                break;
                            }
                        case "ç":
                        case "Ç":
                            {
                                sb.Append("C");
                                break;
                            }
                        default:
                            sb.Append(c.ToString().ToUpper());
                            break;

                    }
                }
                else
                {
                    if (pattern.Any(x => x == c.ToString()))
                    {
                        sb.Append(c.ToString());
                    }
                    sb.Append(c.ToString().ToUpper());
                }
            }

            return sb.ToString();
        }

        // CropEnd - stringin sonunu kırpar
        public static string CropEnd(this string str, string toCrop)
        {
            if (str.IsNullOrEmpty() || toCrop.IsNullOrEmpty())
            {
                return string.Empty;
            }
            else
            {
                if ((str.ToLowerEng().EndsWith(toCrop.ToLowerEng(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    return str.Left(str.Length - toCrop.Length);
                }
                else
                {
                    return str;
                }
            }
        }

        // CropStart - stringin başını kırpar
        public static string CropStart(this string str, string toCrop)
        {
            if (str.IsNullOrEmpty() || toCrop.IsNullOrEmpty())
            {
                return str;
            }
            else
            {
                if ((str.ToLowerEng().StartsWith(toCrop.ToLowerEng(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    return str.Right(str.Length - toCrop.Length);
                }
                else
                {
                    return str;
                }
            }
        }

        // Right - stringin sağından alır
        public static string Right(this string str, int length)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            string sonuc = string.Empty;

            if ((str.Length <= length))
            {
                sonuc = str;
            }
            else
            {
                sonuc = str.Substring(str.Length - length, length);
            }

            return sonuc;
        }

        // Left - stringin solundan alır
        public static string Left(this string str, int length)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            string res = string.Empty;

            if ((str.Length < length))
            {
                return str;
            }

            if ((str.Length <= length))
            {
                res = str;
            }
            else
            {
                res = str.Substring(0, length);
            }

            return res;
        }

        // ToLowerEng - ingilizce küçük harflere çevirir
        public static string ToLowerEng(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            return str.ToLower(true).ToEng();
        }

        // ToLower - küçük harfe çevirir
        public static string ToLower(this string str, bool useEnglish)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            if (str.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder res = new StringBuilder();

            foreach (char c in str)
            {
                if (useEnglish)
                {
                    switch (c.ToString())
                    {
                        case "İ":
                        case "I":
                            res.Append("i");
                            break;

                        case "Ğ":
                            res.Append("g");
                            break;

                        case "Ü":
                            res.Append("u");
                            break;

                        case "Ş":
                            res.Append("s");
                            break;

                        case "Ö":
                            res.Append("o");
                            break;

                        case "Ç":
                            res.Append("c");
                            break;

                        default:
                            res.Append(c.ToString().ToLowerInvariant());
                            break;

                    }
                }
                else
                {
                    res.Append(c.ToString().ToLowerInvariant());
                }
            }

            return res.ToString();
        }

        // ToEng - ingilizce karakterlere çevirir
        public static string ToEng(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            if (str.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder res = new StringBuilder();

            foreach (char c in str)
            {
                switch (c.ToString())
                {
                    case "ı":
                        res.Append("i");
                        break;

                    case "ğ":
                        res.Append("g");
                        break;

                    case "ü":
                        res.Append("u");
                        break;

                    case "ş":
                        res.Append("s");
                        break;

                    case "ö":
                        res.Append("o");
                        break;

                    case "ç":
                        res.Append("c");
                        break;

                    case "İ":
                        res.Append("I");
                        break;

                    case "Ğ":
                        res.Append("G");
                        break;

                    case "Ü":
                        res.Append("U");
                        break;

                    case "Ş":
                        res.Append("S");
                        break;

                    case "Ö":
                        res.Append("O");
                        break;

                    case "Ç":
                        res.Append("C");
                        break;

                    default:
                        res.Append(c.ToString());
                        break;

                }
            }

            return res.ToString();
        }

        // TrimDoubleSpace - string içindeki çift boşlukları teke düşürür
        public static string TrimDoubleSpace(this string str, string spaceChar = " ")
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            string oldStr = str;

            while (oldStr.IndexOf(spaceChar + spaceChar) != -1)
            {
                str = str.Replace(spaceChar + spaceChar, spaceChar);
                if (oldStr.Equals(str)) break;
                oldStr = str;
            }

            return str;
        }

        // TrimDoubleChar - string içindeki çift karakterleri teke düşüşür
        public static string TrimDoubleChar(this string str, string repeatChar)
        {
            if (str.IsNullOrEmpty()) return string.Empty;
            if (repeatChar.IsNullOrEmpty()) repeatChar = " ";

            string oldStr = str;

            while (oldStr.IndexOf($"{repeatChar}{repeatChar}") != -1)
            {
                str = str.Replace($"{repeatChar}{repeatChar}", $"{repeatChar}");
                if (oldStr.Equals(str)) break;
                oldStr = str;
            }

            return str;
        }

        // TrimCrlf - satır sonu karakterlerini trimler
        public static string TrimCrLf(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            string x = str;
            x = x.TrimEnd(' ', (char)13, (char)10, (char)7);

            return x;
        }

        // HasEquals - string içinde herhangi biri geçiyor mu diye bakar
        public static bool HasEquals(this string value, params string[] parameters)
        {
            if (value.IsNullOrEmpty()) return false;

            foreach (var item in parameters)
            {
                if (value.Trim().ToUpperEng() == item.Trim().ToUpperEng()) return true;
            }

            return false;
        }

        // HasContains - string içinde herhangi bir geçiyor mu diye bakar
        public static bool HasContains(this string value, params string[] parameters)
        {
            if (value.IsNullOrEmpty()) return false;

            foreach (var item in parameters)
            {
                if (value.Trim().ToUpperEng().Contains(item.Trim().ToUpperEng())) return true;
            }

            return false;
        }

        // HasContainsTrChars - string içerisinde Türkçe karakter geçiyor mu
        public static bool HasContainsTrChars(this string value)
        {
            if (value.IsNullOrEmpty()) return false;

            foreach (var item in value)
            {
                if (TrChars.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        // LeftOrRight - stringin sağını veya solunu alır
        public static string LeftOrRight(this string str, char separator, bool useLeft = true, bool returnEmpty = false)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            string res = str;

            if (str.IndexOf(separator) != -1)
            {
                if (useLeft) // sol
                {
                    res = str.Substring(0, str.IndexOf(separator));
                }
                else if (str.Contains(separator))
                {
                    res = str.Substring(str.IndexOf(separator) + 1);
                }
                else
                {
                    res = string.Empty;
                }
            }
            else
            {
                if (returnEmpty)
                    res = string.Empty;
            }

            return res;
        }

        // LeftOrRight - stringin sağını veya solunu alır
        public static string LeftOrRight(this string str, string seperator, int leftRightType, bool useBlankIfSeperatorNotFound = false)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            int ndx = str.IndexOf(seperator);
            string res = useBlankIfSeperatorNotFound ? string.Empty : str;

            if (ndx > 0)
            {
                res = leftRightType == 1 ? str.Substring(0, ndx) : str.Substring(ndx + 1);
            }

            return res;
        }

        // TrimInLine - string içindeki boşlukları atar
        public static string TrimInLine(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            return str.Replace(" ", string.Empty).Trim();
        }

        // ToFirstCharUpper - cümlenin ilk harfini büyük yapar
        public static string ToFirstCharUpper(this string str, bool useEnglish)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            return str.First().ToString().ToUpper(useEnglish) + String.Join("", str.Skip(1));
        }

        public static string ToFirstCharLower(this string str, bool useEnglish)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            return str.First().ToString().ToLower(useEnglish) + String.Join("", str.Skip(1));
        }

        public static string remx(this string x, string key)
        {
            if (x.IsNullOrEmpty()) return x;
            if (x.Length < key.Length) return x;

            x = x.Remove(0, x.IndexOf(key) + key.Length);
            return x;
        }

        // ToOnOff - geriye on/off stringi döndürür
        public static string ToOnOff(this bool val, string ifTrue, string ifFalse)
        {
            return val ? ifTrue : ifFalse;
        }

        // ToTileCase - ilk harfleri büyük yapar
        public static string ToTileCase(this string str, bool useEnglish = false)
        {
            string res = str;
            if (res.IsNullOrEmpty()) return string.Empty;

            string[] arr = str.ToLower(useEnglish).TrimDoubleSpace().Split(' ').TrimArray();

            StringBuilder sb = new StringBuilder();
            foreach (var item in arr)
            {
                sb.Append(item.ToFirstCharUpper(useEnglish) + " ");
            }

            return sb.ToString().Trim();
        }

        // ToEntityName - bozuk string ismini entity ismine döndürür
        public static string ToEntityName(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;
            str = str.Replace(".", " ").Replace("-", " ").Replace("_", " ").TrimDoubleSpace().TrimCrLf();
            string[] arr = str.Split(' ');

            StringBuilder sb = new StringBuilder();
            foreach (var item in arr)
            {
                sb.Append(item.ToFirstCharUpper(true));
            }

            return sb.ToString();
        }

        // ToWellContent - content içeriğindeki fazla boş satırları siler
        public static string ToWellContent(this string str)
        {
            return Regex.Replace(str, @"(^\s*(\r|\n)){2,2}", string.Empty, RegexOptions.Multiline);
        }

        // ToWellDateFormat - tarihi formatlar
        public static string ToWellDateFormat(this DateTime dt)
        {
            return dt.ToString().Replace(".", "-").Replace(" ", "_").Replace("/", "-").Replace(":", "-").TrimDoubleSpace("-");
        }

        // CaptureString - string arasını alır
        public static string CaptureString(this string str, string key1, string key2, bool replaceCrlf = true, bool returnEmptyIfKeyNotFound = false, string returnEmptyAs = "-")
        {
            if ((str.IsNullOrEmpty()))
            {
                return returnEmptyAs;
            }
            string k1 = key1;
            string k2 = key2;
            int ndx1 = str.IndexOf(k1);

            if (returnEmptyIfKeyNotFound)
            {
                if (ndx1 == -1)
                {
                    return string.Empty;
                }
            }

            while (ndx1 > -1)
            {
                str = str.Remove(0, ndx1 + k1.Length);
                int ndx2 = str.IndexOf(key2);
                if (ndx2 > -1)
                {
                    str = str.Substring(0, ndx2);
                }
                break;
            }
            if (replaceCrlf)
            {
                return str.TrimCrLf().Replace("\r", " ").Replace("\n", " ").TrimDoubleSpace().Trim();
            }
            else
            {
                return str;
            }
        }

        // ToEscapeXML - stringi XMLe çevirir
        public static string ToEscapeXML(this string unescaped)
        {
            if (unescaped.IsNullOrEmpty()) return string.Empty;
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("tag");
            node.InnerText = unescaped;
            string res = node.OuterXml.Trim().CropStart("<tag>").CropEnd("</tag>");
            res = res.Replace("\"", "&quot;");
            return res;
        }

        // ToEscapeURL - URLdeki boşlukları encoding yapar
        public static string ToEscapeURL(this string url)
        {
            if (url.IsNullOrEmpty()) return string.Empty;
            return Uri.EscapeDataString(url);
        }

        // ToEncrypt - Şifrele
        /*public static string ToEncrypt(this string str, string secretKey = "")
        {
            if (secretKey.IsNullOrEmpty()) secretKey = CompanyKey;
            return CryptoHelper.Instance.EncryptStringAES(str, secretKey);
        }*/

        // ToDecrypt - Şifreyi geri çöz
       /* public static string ToDecrypt(this string str, string secretKey = "")
        {
            try
            {
                if (secretKey.IsNullOrEmpty()) secretKey = CompanyKey;
                return CryptoHelper.Instance.DecryptStringAES(str, secretKey);
            }
            catch
            {
                return str;
            }
        }*/

        // ToSecureString - ClearText parolaları secureString formatına çevirir
        public static SecureString ToSecureString(this string val)
        {
            if (val.IsNullOrEmpty())
                throw new ArgumentNullException("null value");

            var ss = new SecureString();

            foreach (char c in val)
                ss.AppendChar(c);

            ss.MakeReadOnly();

            return ss;
        }

        // ToHash - stringin hashini verir
        public static string ToHash(this string str, string salt = "")
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(str + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }

        // StringContainsNumber - String ifadenin tamamı sayısal mı yada içerisinde sayı varmı sonucunu verir
        public static bool StringContainsNumber(this string str, bool isFullNumber)
        {
            List<string> numbers = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i.ToString());
            }

            try
            {
                if (isFullNumber)
                {
                    foreach (var item in str)
                    {
                        if (!numbers.Any(x => x == item.ToString()))
                        {
                            return false;
                        }
                    }
                    return true;

                }
                else
                {
                    foreach (var item in str)
                    {
                        if (numbers.Any(x => x == item.ToString()))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        // ToMD5 - string ifadeti md5 algoritmasıyla şifreler
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byteStr = Encoding.UTF8.GetBytes(str);
            byteStr = md5.ComputeHash(byteStr);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in byteStr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        // ToRemoveHtmlTags - html taglarını kaldırır
        public static string ToRemoveHtmlTags(this string htmlText)
        {
            Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
            return _htmlRegex.Replace(htmlText, string.Empty);

            char[] array = new char[htmlText.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < htmlText.Length; i++)
            {
                char let = htmlText[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

    }

}
