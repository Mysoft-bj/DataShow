using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
namespace My.Core
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid guid)
        {
            return guid == default(Guid) || guid == Guid.Empty;
        }

    }
}
