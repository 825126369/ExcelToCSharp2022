
namespace uData
{
    public class DefaultTemplate
    {
        public static string GenCodeTemplate = @"
using uData;
using System.Collections.Generic;
using System.Collections;
using uData;

namespace {{ NameSpace }}
{

 {% for file in Files %}

[TableFilePaths(new string[]{
         {{ file.TabFilePaths }}
        },
        typeof({{file.ClassName}}Parser)
        )]
 public class {{file.ClassName}} : I{{file.ClassName}}
    {
        {% for field in file.Fields %}
          /// <summary>
          /// {{ field.Comment }}
          /// </summary>
        public {{ field.FormatType }} {{ field.Name}} { get; private set;}
        {% endfor %}
        
        public {{file.ClassName}}({% for field in file.Fields %}
        {% if field.index != 0 %},{% endif %}{{ field.FormatType }} _{{ field.Name}}{% endfor %}
        )               
        {
        {% for field in file.Fields %}        
            this.{{ field.Name}}=_{{ field.Name}};  {% endfor %}       
        }   

        public  {{file.ClassName}}(){}
    }
   
    public  class {{file.ClassName}}Parser : TableRowFieldParser,IDataParser
    {      

        private {{file.ClassName}} m_{{file.ClassName}};
        public IGameData GetData()
        {
           return m_{{file.ClassName}};
        }
        public void Reload(TableFileRow row)
        { 
            try
            {
                
                {% for field in file.Fields %}
                 {{ field.FormatType }}  _{{ field.Name}} = row.Get_{{ field.TypeMethod }}(row.Values[{{ field.Index }}], ""{{field.DefaultValue}}"");              
                {% endfor %}

                m_{{file.ClassName}} = new {{file.ClassName}}(
                {% for field in file.Fields %}
                {% if field.index != 0 %},{% endif %}_{{ field.Name}} {% endfor %}              
                );

            }
             catch (System.Exception ex)
            {
                 string str = string.Format(""Excel Load Failure. DataRow : {{file.ClassName}}.ID :{0}. ErrorMessage : {1}"",Utility.ParsePrimaryKey(row),ex.ToString());
                 throw new System.Exception(str);
            }

        }
    }

   
{% endfor %}  
}
";
    }
}

