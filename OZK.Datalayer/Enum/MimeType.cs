using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OZK.Datalayer.Enum
{
    public enum MimeType
    {
        [EnumMember(Value= "text/csv")]
        Csv,
        [EnumMember(Value = "application/msword")]
        Doc,
        [EnumMember(Value = "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        Docx,
        [EnumMember(Value = "text/html")]
        Html,
        [EnumMember(Value = "application/json")]
        Json,
        [EnumMember(Value = "	application/pdf")]
        Pdf,
        [EnumMember(Value = "text/plain")]
        Txt,
        [EnumMember(Value = "application/vnd.ms-excel")]
        Xls,
        [EnumMember(Value = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        Xlsx,
        [EnumMember(Value = "application/xml")]
        Xml
    }
}
