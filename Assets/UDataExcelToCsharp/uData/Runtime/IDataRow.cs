using uData;


namespace uData
{
    public interface IDataParser
    {
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="_row"></param>
        void Reload(TableFileRow _row);

        /// <summary>
        /// 获取 数据本身
        /// </summary>
        /// <returns></returns>
        IGameData GetData();
    }
    
    public interface IGameData
    {
        int Id { get; }
    }
}