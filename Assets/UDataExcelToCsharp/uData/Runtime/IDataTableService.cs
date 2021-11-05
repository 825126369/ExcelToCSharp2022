using System;

namespace uData
{
    public interface IDataTableService
    {
        DataTableService.DataTable<T> LoadDataTable<T>(string dataTSV) where T:  IGameData;
        DataTableService.DataTable<T> GetDataTable<T>()where T :IGameData;
        
        T GetData<T>(int _id) where T :IGameData;

        T[] GetAllDatas<T>() where T : IGameData;

        T[] GetAllDatas<T>(Predicate<T> _condition) where T:IGameData;

        int GetDataCount<T>() where T : IGameData;

        T GetMinIdData<T>() where T : IGameData;

        T GetMaxIdData<T>() where T : IGameData;

        bool HasDataTable<T>() where T : IGameData;
    }

}


