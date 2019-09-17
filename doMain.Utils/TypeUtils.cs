using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public class TypeUtils
    {

        public static string ToNormalType(string systemtype)
        {
            string type = systemtype;
            switch (systemtype)
            {
                case "Boolean": type = "bool"; break;
                case "Int32": type = "int"; break;
                case "String": type = "string"; break;
                case "Decimal": type = "decimal"; break;
            }



            return type;
        }

        public static string ToSystemType(string datatype)
        {
            string type = "";
            switch (datatype)
            {
                case "bigint": type = "long"; break;
                case "binary": type = "byte[]"; break;
                case "blob": type = "byte[]"; break;
                case "blob sub_type 1":
                    type = "byte[]"; break;

                   
                case "bit": type = "bool"; break;
                case "char": type = "string"; break;
                case "date": type = "DateTime"; break;
                case "datetime": type = "DateTime"; break;
                case "datetime2": type = "DateTime"; break;
                case "datetimeoffset": type = "DateTimeOffset"; break;
                case "decimal": type = "decimal"; break;
                case "filestream": type = "byte[]"; break;
                case "float": type = "double"; break;
                //case "geography": type = "Microsoft.SqlServer.Types.SqlGeography"; break;
                //case "geometry": type = "Microsoft.SqlServer.Types.SqlGeometry"; break;
                //case "hierarchyid": type = "Microsoft.SqlServer.Types.SqlHierarchyId"; break;
                case "image": type = "byte[]"; break;
                case "int": type = "int"; break;
                case "NUMBER": type = "int"; break;
                case "VARCHAR2": type = "string"; break;
                case "DATE": type = "DateTime"; break;
                case "integer": type = "int"; break;
                case "money": type = "decimal"; break;
                case "nchar": type = "string"; break;
                case "ntext": type = "string"; break;
                case "numeric": type = "decimal"; break;
                case "nvarchar": type = "string"; break;
                case "real": type = "Single"; break;
                case "rowversion": type = "byte[]"; break;
                case "smalldatetime": type = "DateTime"; break;
                case "smallint": type = "short"; break;
                case "smallmoney": type = "decimal"; break;
                case "sql_variant": type = "object"; break;
                case "text": type = "string"; break;
                case "time": type = "TimeSpan"; break;
                case "timestamp": type = "DateTime"; break;
                case "tinyint": type = "byte"; break;
                case "uniqueidentifier": type = "Guid"; break;
                case "varbinary": type = "byte[]"; break;
                case "varchar": type = "string"; break;
                case "xml": type = "string"; break;
                case "double precision": type = "double"; break;

                default: type = "object"; break;
            }



            return type;
        }
    }
}
