using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQMFramework.Utils.Common
{
    public class BizResult
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public BizResult()
        {

        }

        public BizResult(int code, string msg)
        {
            this.Code = code;
            this.Message = msg;
        }

        public void SetCodeAndMessage(int code,string msg)
        {
            Code = code;
            Message = msg;
        }

        public void SetCodeAndMessage(BizException ex)
        {
            Code = ex.Code;
            Message = ex.Message;
        }
    }

    public class BizResult<T> : BizResult
    {
        public T ReturnObject { get; set; }
    }
}
