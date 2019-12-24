using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace QLTB.Utility
{
    public class MaHoaSHA1
    {
        public string EncodeSHA1(string pass)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
            bs = sha1.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToUpper());
            }
            pass = s.ToString();
            return pass;
        }
    }
   
    public static class UnsignUnicode
    {
        public static  string NonUnicode(this string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",  
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",  
            "í","ì","ỉ","ĩ","ị",  
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",  
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",  
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",  
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",  
            "i","i","i","i","i",  
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",  
            "u","u","u","u","u","u","u","u","u","u","u",  
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }  
    }
    public static class EntityToTable
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> entityList) where T : class
        {
            var properties = typeof(T).GetProperties();
            var table = new DataTable();

            foreach (var property in properties)
            {
                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                table.Columns.Add(property.Name, type);
            }
            foreach (var entity in entityList)
            {
                table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            }
            return table;
        }
    }

    //public static class Helper
    //{
    //    public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
    //    {
    //        try
    //        {
    //            List<T> list = new List<T>();

    //            foreach (var row in table.AsEnumerable())
    //            {
    //                T obj = new T();

    //                foreach (var prop in obj.GetType().GetProperties())
    //                {
    //                    try
    //                    {
    //                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
    //                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
    //                    }
    //                    catch
    //                    {
    //                        continue;
    //                    }
    //                }

    //                list.Add(obj);
    //            }

    //            return list;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //}
    #region Các hàm xử lý chuỗi
    public class XulyChuoi
    {
        public bool ktEmail(string email)
        {
            string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(match);
            if (reg.IsMatch(email))
                return true;
            else
                return false;
        }
        public string Ten(string hoten)
        {
            int n = hoten.LastIndexOf(" ");
            return hoten.Substring(n + 1, hoten.Length - n - 1);
        }
        public string HoLot(string hoten)
        {
            int n = hoten.LastIndexOf(" ");
            return hoten.Substring(0, n);
        }
        public string HoaDauTu(string hoten)
        {
            const char Space = ' ';
            string s = hoten.Trim();
            while (s.IndexOf("  ") >= 0)
                s = s.Replace("  ", " ");
            string kq = "";
            if (s.Length == 0)
                return kq;
            else
            {
                kq = "";
                string[] s1 = s.Split(Space);
                foreach (string tu in s1)
                {
                    string s2 = tu[0].ToString();
                    kq += s2.ToUpper();
                    kq += tu.Substring(1, tu.Length - 1).ToLower();
                    kq += " ";
                }
                return kq.Trim();
            }
        }
        public string getName(string d, int chuoicon, int vitri)
        {
            string s;
            string[] parts = d.Split(';');
            string p = parts[chuoicon];
            string[] ps = p.Split('=');
            return s = ps[vitri];
        }
        public bool IsDateTime(string d)
        {
            DateTime tempDate;

            return DateTime.TryParse(d, out tempDate) ? true : false;
        }
        public string ConvertDateTime_ddMMyyyy(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[0], parts[1], parts[2]);
            return dt;
        }
        public string ConvertDateTime_MMddyyyy(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
    }
    #endregion
}
