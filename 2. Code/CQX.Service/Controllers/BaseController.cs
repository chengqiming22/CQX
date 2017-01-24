using CQMFramework.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CQX.Service.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected async Task<BizResult> InvokeWithCatchAsync(Action action)
        {
            return await Task.Run(() =>
            {
                var result = new BizResult();
                try
                {
                    action();
                }
                catch (BizException ex)
                {
                    result.SetCodeAndMessage(ex);
                }
                catch (Exception ex)
                {
                    result.SetCodeAndMessage(-1, ex.Message);
                }
                return result;
            });
        }

        protected async Task<BizResult<T>> InvokeWithCatchAsync<T>(Func<T> func)
        {
            return await Task.Run(() =>
            {
                var result = new BizResult<T>();
                try
                {
                    result.ReturnObject = func();
                }
                catch (BizException ex)
                {
                    result.SetCodeAndMessage(ex);
                }
                catch (Exception ex)
                {
                    result.SetCodeAndMessage(-1, ex.Message);
                }
                return result;
            });
        }
    }
}