using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Core.Sql
{
    static class Singleton<T> where T : new()
    {
        public static T Instance = new T();
    }
}
