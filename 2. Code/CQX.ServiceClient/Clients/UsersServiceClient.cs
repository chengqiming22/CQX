using CQX.ServiceContract.Models;
using CQMFramework.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.ServiceClient.Clients
{
    public class UsersServiceClient : ServiceClientBase
    {
        public async Task<BizResult<UserModel>> CreateUserAsync(UserModel userModel)
        {
            return await PostAsync<BizResult<UserModel>>(userModel, "api/users");
        }

        public async Task<BizResult<UserModel>> GetUserAsync(int userId)
        {
            return await GetAsync<BizResult<UserModel>>("api/users?id={0}", userId);
        }

        public BizResult<UserModel> GetUser(string userName)
        {
            return Get<BizResult<UserModel>>("api/users?userName={0}", userName);
        }
        public async Task<BizResult<UserModel>> GetUserAsync(string userName)
        {
            return await GetAsync<BizResult<UserModel>>("api/users?userName={0}", userName);
        }

        public BizResult<List<ModuleModel>> GetPagesByUserName(string userName)
        {
            return Get<BizResult<List<ModuleModel>>>("api/users/{0}/pages", userName);
        }
    }
}
