using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DbManager
{
    private Dictionary<Type, List<DbBase>> mDbDic = new Dictionary<Type, List<DbBase>>();

    public List<T> GetDb<T>() where T : DbBase
    {
        List<T> mDbList = new List<T>();
        if (mDbDic.ContainsKey(typeof(T)))
        {
            List<DbBase> mDb = mDbDic[typeof(T)];
            foreach (T t in mDb)
            {
                mDbList.Add(t);
            }
        }
        return mDbList;
    }

    public void addDb(Type mType, DbBase mSheet)
    {
        if (!mDbDic.ContainsKey(mType))
        {
            mDbDic.Add(mType, new List<DbBase>());
        }
        mDbDic[mType].Add(mSheet);
    }

    private void OnDestroy()
    {
        mDbDic.Clear();
        mDbDic = null;
    }
}

public class DbBase
{
    public void SetDbValue(Dictionary<string, string> list)
    {
        FieldInfo[] mFieldInfo = this.GetType().GetFields();
        for (int i = 0; i < mFieldInfo.Length; i++)
        {
            FieldInfo mField = mFieldInfo[i];
            string key = mField.Name;
            string value = "";
            if (list.ContainsKey(key))
            {
                value = list[key];
            }
            else
            {
                Debug.LogError("配置表 xml与脚本不对应: " + mField.Name);
                continue;
            }
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }
            if (mField.FieldType.IsArray)
            {
                string[] valueArray = value.Split('#');
                Array mArray = null;
                mArray = mField.GetValue(this) as Array;
                if (mArray == null)
                {
                    int Length = valueArray.Length;
                    mArray = Array.CreateInstance(mField.FieldType.GetElementType(), Length);
                    for (int j = 0; j < mArray.Length; j++)
                    {
                        mArray.SetValue(GetFieldValue(mField.FieldType.GetElementType(), valueArray[j]), j);
                    }
                }
                for (int j = 0; j < valueArray.Length; j++)
                {
                    mArray.SetValue(GetFieldValue(mField.FieldType.GetElementType(), valueArray[j]), j);
                }
                mField.SetValue(this, mArray);

            }
            else if (mField.FieldType.IsGenericType)
            {
                if (mField.FieldType == typeof(List<int>))
                {
                    List<int> mGentericList = mField.GetValue(this) as List<int>;
                    if (mGentericList == null)
                    {
                        mGentericList = new List<int>();
                    }
                    string[] valueArray2 = value.Split('#');
                    foreach (string s in valueArray2)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            mGentericList.Add((int)GetFieldValue(typeof(int), s));
                        }
                    }
                    mField.SetValue(this, mGentericList);
                }
                else if (mField.FieldType == typeof(List<string>))
                {
                    List<string> mGentericList = mField.GetValue(this) as List<string>;
                    if (mGentericList == null)
                    {
                        mGentericList = new List<string>();
                    }
                    string[] valueArray2 = value.Split('#');
                    foreach (string s in valueArray2)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            mGentericList.Add(s);
                        }
                    }
                    mField.SetValue(this, mGentericList);
                }
                else
                {
                    Debug.LogError("不能识别的类型");
                }
            }
            else
            {
                mField.SetValue(this, GetFieldValue(mField.FieldType, value));
            }
        }
    }

    public object GetFieldValue(System.Type type, string value)
    {
        if (type == typeof(int))
        {
            int nGetValue = 0;
            int.TryParse(value, out nGetValue);
            return nGetValue;
        }
        else if (type == typeof(string))
        {
            return value;
        }
        else if (type == typeof(float))
        {
            float nGetValue = 0;
            float.TryParse(value, out nGetValue);
            return nGetValue;
        }
        else if (type == typeof(double))
        {
            double nGetValue = 0;
            double.TryParse(value, out nGetValue);
            return nGetValue;
        }
        else if (type == typeof(bool))
        {
            bool nGetValue = false;
            bool.TryParse(value, out nGetValue);
            return nGetValue;
        }
        else if (type.BaseType == typeof(Enum))
        {
            return Enum.Parse(type, value);
        }
        return null;
    }

    public void PrintDbInfo()
    {
        FieldInfo[] mFieldInfo = this.GetType().GetFields();
        Debug.Log(this.GetType().ToString() + ":");
        for (int i = 0; i < mFieldInfo.Length; i++)
        {
            FieldInfo mField = mFieldInfo[i];
            if (mField.FieldType.IsArray)
            {
                Array mArray = (Array)mField.GetValue(this);
                for (int j = 0; j < mArray.Length; j++)
                {
                    Debug.Log(mField.Name + "[" + j + "]: " + mArray.GetValue(j));
                }
            }
            else if (mField.FieldType.IsGenericType)
            {
                IList mlist = mField.GetValue(this) as IList;
                for (int j = 0; j < mlist.Count; j++)
                {
                    Debug.Log(mField.Name + "[" + j + "]: " + mlist[j]);
                }
            }
            else
            {
                Debug.Log(mField.Name + ": " + mField.GetValue(this));
            }
        }
    }
}