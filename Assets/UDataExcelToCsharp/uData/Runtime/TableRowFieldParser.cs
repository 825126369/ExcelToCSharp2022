using System;
using System.Collections.Generic;
using System.Linq;

namespace uData
{
    /// <summary>
    /// Field parser, string to something, user can custom it with extensions
    /// </summary>
    public partial class TableRowFieldParser
    {
        public string Get_String(string value, string defaultValue)
        {
            return Get_string(value, defaultValue);
        }

        public string Get_string(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value;
        }

        public double Get_number(string value, string defaultValue)
        {
            return Get_double(value, defaultValue);
        }

        public double Get_Number(string value, string defaultValue)
        {
            return Get_double(value, defaultValue);
        }

        public int Get_Int32(string value, string defaultValue)
        {
            return Get_int(value, defaultValue);
        }

        public bool Get_bool(string value, string defaultValue)
        {
            return Get_Boolean(value, defaultValue);
        }

        public bool Get_Boolean(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            bool result;
            if (bool.TryParse(str, out result))
            {
                return result;
            }
            return Get_int(value, defaultValue) != 0;
        }

        public long Get_long(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(long) : long.Parse(str);
        }
        public int Get_int(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(int) : int.Parse(str);
        }
        public int[] Get_int_array(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            string[] strs = str.Split('|', ',');
            int[] ret = new int[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                ret[i] = string.IsNullOrEmpty(strs[i]) ? default(int) : int.Parse(strs[i]);
            }
            return ret;
        }
        
        public object[] Get_object_array(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            string[] strs = str.Split('|', ',');
            object[] ret = new object[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                if(string.IsNullOrEmpty(strs[i]))
                {
                    ret[i] = null;
                }else
                {
                 
                    int vint;
                    if (int.TryParse(strs[i], out vint))
                    {
                        ret[i] = vint;
                        continue;
                    }
                    bool vbool;
                    if (bool.TryParse(strs[i], out vbool))
                    {
                        ret[i]  = vbool;
                        continue;
                    }
                    float vfloat;
                    if(float.TryParse(strs[i],out vfloat))
                    {
                        ret[i] = vfloat;
                        continue;
                    }
                    ret[i] = strs[i];
                }
            }
            return ret;
        }

        public double Get_double(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(double) : double.Parse(str);
        }

        public float Get_float(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(float) : float.Parse(str);
        }
        public uint Get_uint(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(int) : uint.Parse(str);
        }

        public string[] Get_string_array(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return str.Split('|', ',');
        }
        
        public Dictionary<TKey, TValue> Get_Dictionary<TKey, TValue>(string value, string defaultValue)
        {
            var dict = new Dictionary<TKey, TValue>();
            var str = Get_String(value, defaultValue);
            var arr = str.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var item in arr)
            {
                var kv = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var itemKey = ConvertString<TKey>(kv[0]);
                var itemValue = ConvertString<TValue>(kv[1]);
                dict[itemKey] = itemValue;
            }
            return dict;
        }

        protected T ConvertString<T>(string value)
        {
            if (typeof(Enum).IsAssignableFrom(typeof(T)))
            {
                return Get_Enum<T>(value);
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        protected T Get_Enum<T>(string value)
        {
           return (T)Enum.Parse(typeof(T), value);
        }

        public T Get_Enum<T>(string value , string defaultValue)
        {
            if(string.IsNullOrEmpty(value)) return Get_Enum<T>(defaultValue);
            return Get_Enum<T>(value);
        }
        
        public T Get_Instance<T>(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value)) return UnityEngine.JsonUtility.FromJson<T>(defaultValue);
            return UnityEngine.JsonUtility.FromJson<T>(value);
        }
            


    }

}
