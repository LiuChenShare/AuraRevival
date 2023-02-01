using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AuraRevival.Core
{
    public static class ExpandMethod
    {
        /// <summary>
        /// 获取翻译
        /// </summary>
        /// <param name="enumInfo"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumInfo)
        {
            Type enumType = enumInfo.GetType();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FieldInfo[] fieldinfos = enumType.GetFields();
            foreach (FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);
                }

            }


            return dic[enumInfo.ToString()];
        }
    }
}
