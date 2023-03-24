using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRevival.DB
{
    public class DBFieldTypeAttribute : Attribute
    {
        public Type Type { get; set; }

        public DBFieldTypeAttribute()
        {
            //Console.WriteLine(nameof(DBFieldTypettribute));
        }
        public DBFieldTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
