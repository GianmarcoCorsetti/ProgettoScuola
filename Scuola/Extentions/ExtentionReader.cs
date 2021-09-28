using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Extentions {
    public static class ReaderExtention {
        public static int GetInt32(this SqlDataReader reader, string name)
        {
            return reader.GetInt32(reader.GetOrdinal(name));
        }
        public static bool GetBoolean(this SqlDataReader reader, string name)
        {
            return reader.GetBoolean(reader.GetOrdinal(name));
        }
        public static string GetString(this SqlDataReader reader, string name)
        {
            return reader.GetString(reader.GetOrdinal(name));
        }
        public static bool? GetNullableBoolean(this SqlDataReader reader, string name)
        {
            return reader.IsDBNull(name) ? null : reader.GetBoolean(name);
        }
    }
}
