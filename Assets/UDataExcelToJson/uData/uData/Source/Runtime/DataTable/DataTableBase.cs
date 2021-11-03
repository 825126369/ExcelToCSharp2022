using System;
using TableML;


namespace uData.DataTable
{

    public abstract class DataTableBase:IDisposable
    {

        private readonly string m_Name;
#if UNITY_EDITOR && HOTEXCEL
        public abstract event EventHandler<EditorHotUpdateEventArg> HoTUpdateEventArg;
#endif


        public DataTableBase()
       : this(null)
        {
        }
        public DataTableBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public abstract Type DataType {
            get;
        }

        public abstract IGameData this[int id]
        {
            get;
        }
   
        public abstract int Count
        {
            get;
        }
      public abstract  IGameData MinIdData { get; }

      public abstract  IGameData MaxIdData { get; }

      public abstract bool HasData(int id);

      public abstract  IGameData GetData(int id);

      public abstract  IGameData[] GetAllDatas();


        public abstract void ReloadAll (TableFile _file );

        public abstract void Dispose();
       
    }
}

