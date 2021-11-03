

using System;
using System.Collections.Generic;
using TableML;

namespace uData.DataTable
{
    public interface IDataTable<T>
        where T :  IGameData
    {
        string Name { get; }

        Type DataTableType { get; }

        int Count { get; }

       // T this[int id] { get; }
      
        T MinIdData { get; }

        T MaxIdData { get; }

        bool HasData(int id);

        T GetData(int id);

        T[] GetAllDatas();

        void ReloadAll(TableFile _file );


    }

}


