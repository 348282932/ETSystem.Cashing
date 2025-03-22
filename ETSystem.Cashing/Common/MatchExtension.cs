using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ETSystem.Cashing.Common
{
    public static class MatchExtension
    {
        public static string GetResult(this Match match, string replacement)
        {
            if (match.Success) return match.Result(replacement);

            return string.Empty;
        }

        public static T GetResult<T>(this Match match, string replacement)
        {
            var strResult = match.GetResult(replacement);

            if (!string.IsNullOrEmpty(strResult)) 
            {
                // 反射获取 TryParse 方法

                var type = typeof(T);

                // 泛型 Nullable 判断，取其中的类型

                if (type.IsGenericType)
                {
                    type = type.GetGenericArguments()[0];
                }

                if (type.Name.ToLower() == "string") return (T)(object)strResult;

                var TryParse = type.GetMethod("TryParse", 
                                                BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder,
                                                new Type[] { typeof(string), type.MakeByRefType() },
                                                new ParameterModifier[] { new ParameterModifier(2) });

                var parameters = new object[] { strResult, Activator.CreateInstance(type) };

                bool success = (bool)TryParse.Invoke(null, parameters);

                if (success) return (T)parameters[1];
            }

            return default;
        }
    }
}
