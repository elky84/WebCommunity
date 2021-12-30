﻿using MongoDbWebUtil.Models;

namespace WebUtil.Models
{
    public static class ModelsExtend
    {
        public static T ToProtocol<T>(this T t, MongoDbHeader header)
            where T : EzAspDotNet.Protocols.Common.CommonHeader
        {
            t.Id = header.Id;
            t.Created = header.Created;
            t.Updated = header.Updated;
            return t;
        }
    }
}