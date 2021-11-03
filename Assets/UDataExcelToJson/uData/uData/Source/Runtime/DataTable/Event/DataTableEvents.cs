using System;

namespace uData.DataTable
{
#if UNITY_EDITOR && HOTEXCEL
    public class EditorHotUpdateEventArg :System.EventArgs
    {
        public EditorHotUpdateEventArg(string _name, Type _type)
        {
            this.Name = _name;
            this.Type = _type;
        }
        public Type Type
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
    }
#endif
    public class LoadDataTableSuccessEventArg 
    {

        public LoadDataTableSuccessEventArg(string dataTableAssetName, float duration, object userData)
        {
            DataTableAssetName = dataTableAssetName;
            Duration = duration;
            UserData = userData;
        }

        public string DataTableAssetName
        {
            get;
            private set;
        }


        public float Duration
        {
            get;
            private set;
        }


        public object UserData
        {
            get;
            private set;
        }
    }

    public class LoadDataTableFailureEventArg 
    {
        public LoadDataTableFailureEventArg(string dataTableAssetName, string errorMessage, object userData)
        {
            DataTableAssetName = dataTableAssetName;
            ErrorMessage = errorMessage;
            UserData = userData;
        }


        public string DataTableAssetName
        {
            get;
            private set;
        }


        public string ErrorMessage
        {
            get;
            private set;
        }


        public object UserData
        {
            get;
            private set;
        }
    }
    public class LoadDataTableUpdateEventArg
    {
        public LoadDataTableUpdateEventArg(string dataTableAssetName, float progress, object userData)
        {
            DataTableAssetName = dataTableAssetName;
            Progress = progress;
            UserData = userData;
        }


        public string DataTableAssetName
        {
            get;
            private set;
        }


        public float Progress
        {
            get;
            private set;
        }


        public object UserData
        {
            get;
            private set;
        }
    }

    public struct LoadDataTableCmd
    {
        public Type Type;
        public object UserData;
    }
}

