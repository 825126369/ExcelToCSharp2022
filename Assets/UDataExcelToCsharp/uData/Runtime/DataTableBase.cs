using System;
using uData;


namespace uData
{
    public abstract class DataTableBase
    {
        private readonly string m_Name;
        
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

    }
}

