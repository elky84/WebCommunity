using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebUtil.Util;

namespace WebUtil.StartUp
{
    public class CustomJsonResolver : CamelCasePropertyNamesContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                return new DefaultContractResolver().ResolveContract(type);
            }
            else
            {
                return base.ResolveContract(type);
            }
        }

    }
}
