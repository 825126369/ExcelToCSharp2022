#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;

namespace uData.DataTable
{
    public sealed class DirectoryWatcher
    {

        static readonly Dictionary<string, FileSystemWatcher> _watchers = new Dictionary<string, FileSystemWatcher>();

        /// <summary>
        /// 监视一个目录，如果有修改则触发事件函数, 包含其子目录！
        /// <para>使用更大的buffer size确保及时触发事件</para>
        /// <para>不用includesubdirect参数，使用自己的子目录扫描，更稳健</para>
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public DirectoryWatcher(string dirPath, FileSystemEventHandler handler)
        {
            CreateWatch(dirPath, handler);
        }

        void CreateWatch(string dirPath, FileSystemEventHandler handler)
        {
            if (_watchers.ContainsKey(dirPath))
            {
                _watchers[dirPath].Dispose();
                _watchers[dirPath] = null;
            }

            if (!Directory.Exists(dirPath)) return;

            var watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = false;//includeSubdirectories;
            watcher.Path = dirPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*";
            watcher.Changed += handler;
            watcher.EnableRaisingEvents = true;
            watcher.InternalBufferSize = 10240;
            //return watcher;
            _watchers[dirPath] = watcher;


            foreach (var childDirPath in Directory.GetDirectories(dirPath))
            {
                CreateWatch(childDirPath, handler);
            }
        }
    }
}


#endif
