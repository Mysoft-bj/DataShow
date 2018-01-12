using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
   public static class SerializeHelper
    {
       public static byte[] Serialize<T>(T obj)
       {
           using (var memStream = new MemoryStream())
           {
               ProtoBuf.Serializer.Serialize(memStream, obj);
               return memStream.ToArray();
           }
       }
       public static T Deserialize<T>(byte[] byteArray)
       {
           using (var memStream = new MemoryStream(byteArray))
           {
               return ProtoBuf.Serializer.Deserialize<T>(memStream);
           }
       }

    }
}
