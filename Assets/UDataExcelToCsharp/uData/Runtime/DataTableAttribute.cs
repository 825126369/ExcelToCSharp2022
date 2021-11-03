
using System;
namespace uData
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Interface)]
    public class TableFilePathsAttribute : Attribute
    {

        public string[] TableFilePaths { get; private set; }

        public Type ParserType;

        public TableFilePathsAttribute(string[] _paths,Type _type)
        {
            this.TableFilePaths = _paths;
            this.ParserType = _type;
        }
    
    }
}


