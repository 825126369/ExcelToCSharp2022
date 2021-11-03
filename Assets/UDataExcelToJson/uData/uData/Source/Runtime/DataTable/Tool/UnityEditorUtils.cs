
#if UNITY_EDITOR

using System;
using System.Collections.Generic;
namespace uData.DataTable
{
    public class UnityEditorUtils
    {


        internal static Queue<Action> _mainThreadActions = new Queue<Action>();

        static UnityEditorUtils()
        {
            UnityCatch.OnEditorUpdateEvent -= OnEditorUpdate;
            UnityCatch.OnEditorUpdateEvent += OnEditorUpdate;
        }

        /// <summary>
        /// 捕获Unity Editor update事件
        /// </summary>
        private static void OnEditorUpdate()
        {
            // 主线程委托
            while (_mainThreadActions.Count > 0)
            {
                var action = _mainThreadActions.Dequeue();
                if (action != null) action();
            }
        }


        public static void CallMainThread(Action action)
        {
            _mainThreadActions.Enqueue(action);
        }
    }

}


#endif
