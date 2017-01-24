using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.Web.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, int>, IQueryableRoleStore<IdentityRole, int>
    {
        #region IRoleStore<IdentityRole,int> 成员

        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IQueryableRoleStore<IdentityRole,int> 成员

        public IQueryable<IdentityRole> Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
