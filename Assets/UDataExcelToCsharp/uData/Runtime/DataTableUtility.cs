using System;
using System.Linq;
using UnityEngine;

namespace uData
{
    public static partial class Utility
    {
        public static int ParsePrimaryKey(TableFileRow row)
        {
            var primaryKey = row.Get_int(row.Values[0], "");
            return primaryKey;
        }

        public static bool IsFileSystemMode
        {
            get
            {
#if UNITY_EDITOR
                    return true;
#else
                return false;
#endif


            }
        }

        public static TableFilePathsAttribute GetTableFilesAttributes(this Type _type)
        {
            var attr = (TableFilePathsAttribute)_type.GetCustomAttributes(typeof(TableFilePathsAttribute), false).FirstOrDefault();
            if (attr == null)
            {
                Debug.LogError("you clas not TableFilePaths  :" + _type.Name);
            }
            return attr;
        }

    }


}




