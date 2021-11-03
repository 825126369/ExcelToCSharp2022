

using System.Collections.Generic;
using System.IO;
using TableML;
using System;
using System.Linq;
using UnityEngine;

namespace uData.DataTable
{
 
        public static partial  class Utility
        {
#if UNITY_EDITOR
        public const string ByteFilePath = "../Product/Data/";
#else
        public const string ByteFilePath = "";
#endif
        public const string ExcelFiulePath = "../HidenObjectData/Product/DataSource";


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
            private static string GetFileSystemPath(string path)
            {
                var compilePath = ByteFilePath;// DT_ServiceEditor.SettingCompilePath;// AppEngine.GetConfig("KEngine.Setting", "SettingCompiledPath"); 
                var resPath = Path.Combine(compilePath, path);
                return resPath;
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

        public static partial class Utility
        {
#if UNITY_EDITOR
            public static class Watch
            {
                private static Dictionary<string, FileSystemWatcher> _cacheWatchers;

                public static void WatchDataTable(string path, System.Action<string> action)
                {
                    if (!IsFileSystemMode)
                    {
                        return;
                    }
                    if (_cacheWatchers == null)
                        _cacheWatchers = new Dictionary<string, FileSystemWatcher>();
                    FileSystemWatcher watcher;
                    var dirPath = Path.GetDirectoryName(GetFileSystemPath(path));
                    dirPath = dirPath.Replace("\\", "/");

                    if (!Directory.Exists(dirPath))
                    {

                        return;
                    }
                    if (!_cacheWatchers.TryGetValue(dirPath, out watcher))
                    {
                        _cacheWatchers[dirPath] = watcher = new FileSystemWatcher(dirPath);

                    }

                    watcher.IncludeSubdirectories = false;
                    watcher.Path = dirPath;
                    watcher.NotifyFilter = NotifyFilters.LastWrite;
                    watcher.Filter = "*";
                    watcher.EnableRaisingEvents = true;
                    watcher.InternalBufferSize = 2048;
                    watcher.Changed += (sender, e) =>
                    {
                        
                        action(path);
                    };
                }

            }
#endif


        }


    }




