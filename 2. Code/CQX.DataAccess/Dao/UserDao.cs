using CQX.DataModel.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataAccess.Dao
{
    public class UserDao : BaseDao<User>
    {
        public User Get(string userName)
        {
            using (var db = new CQXDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userName && u.IsActive);
                return user;
            }
        }
    }
}
