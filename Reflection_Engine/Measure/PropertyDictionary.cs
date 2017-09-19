﻿using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Reflection
{
    public static partial class Measure
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Dictionary<string, object> GetPropertyDictionary(this BHoMObject obj)
        {
            return _GetPropertyDictionary(obj as dynamic);
        }


        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        public static Dictionary<string, object> _GetPropertyDictionary(this BHoMObject obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite || prop.GetMethod.GetParameters().Count() > 0) continue;
                var value = prop.GetValue(obj, null);
                if (value != null && !(value is ValueType))
                    dic[prop.Name] = value;
            }
            return dic;
        }

        /***************************************************/

        public static Dictionary<string, object> _GetPropertyDictionary(this CustomObject obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>(obj.CustomData);

            if (!dic.ContainsKey("Name"))
                dic["Name"] = obj.Name;

            if (!dic.ContainsKey("BHoM_Guid"))
                dic["BHoM_Guid"] = obj.BHoM_Guid;

            if (!dic.ContainsKey("Tags"))
                dic["Tags"] = obj.Tags;

            return dic;
        }
    }
}