using CQX.DataAccess.Dao;
using CQX.DataModel.Entites;
using CQX.ServiceContract.Models;
using CQMFramework.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CQX.Service.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        public async Task<BizResult<UserModel>> Post(UserModel userModel)
        {
            return await InvokeWithCatchAsync(() =>
            {
                if (userModel == null)
                {
                    throw new BizException(-1, "用户信息不能为空");
                }

                var user = userModel.ConvertTo<User>();
                user.IsActive = true;
                if (!new UserDao().Create(user))
                {
                    throw new BizException(-1, "创建用户失败");
                }
                return user.ConvertTo<UserModel>();
            });
        }

        public async Task<BizResult<UserModel>> Get(int id)
        {
            return await InvokeWithCatchAsync(() =>
            {
                if (id <= 0)
                {
                    throw new BizException(-1, "用户Id不能为空");
                }

                var user = new UserDao().Get(id);
                if (user == null || !user.IsActive)
                {
                    throw new BizException(-1, "根据用户Id【{0}】未查询到用户信息", id);
                }
                return user.ConvertTo<UserModel>();
            });
        }

        public async Task<BizResult<UserModel>> Get(string userName)
        {
            return await InvokeWithCatchAsync(() =>
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new BizException(-1, "用户名不能为空");
                }

                var user = new UserDao().Get(userName);
                if (user == null)
                {
                    throw new BizException(-1, "根据用户名【{0}】未查询到用户信息", userName);
                }
                return user.ConvertTo<UserModel>();
            });
        }

        [Route("{userName}/pages")]
        public async Task<BizResult<List<ModuleModel>>> GetPagesByUserName(string userName)
        {
            return await InvokeWithCatchAsync(() =>
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new BizException(-1, "用户名不能为空");
                }

                var pages = new PageDao().GetPagesByUserName(userName);
                if (pages == null || pages.Count == 0)
                {
                    throw new BizException(-1, "根据用户名【{0}】未查询到结果", userName);
                }

                var groups = pages.Select(p => p.Group).Distinct().ListCovnertTo<ModuleModel>().ToList();
                return groups;
            });
        }
    }
}
