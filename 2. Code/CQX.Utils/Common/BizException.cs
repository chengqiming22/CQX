using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQMFramework.Utils.Common
{
    public class BizException : Exception
    {
        public int Code { get; private set; }

        public BizException(int code, string message, params object[] args)
            : base(string.Format(message, args))
        {
            Code = code;
        }
    }
}
