using CQX.DataModel.Entites;
using CQX.DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataAccess.Dao
{
    public class PageDao : BaseDao<Page>
    {
        public List<Page> GetPagesByUserName(string userName)
        {
            using (var db = new CQXDbContext())
            {
                var user = db.Users.Include("Group").FirstOrDefault(u => u.UserName == userName);
                if (user == null || user.Group == null)
                    return null;
                var pageIds = new List<long>();
                db.Entry(user.Group).Collection(g => g.Roles).Load();
                foreach (var role in user.Group.Roles)
                {
                    db.Entry(role).Collection(r => r.Permissions).Load();
                    pageIds.AddRange(role.Permissions.Where(p => p.Type == (short)PermissionType.Page).Select(p => p.ResourceId));
                }
                pageIds = pageIds.Distinct().ToList();
                var pages = db.Pages.Include("Group").Where(p => pageIds.Contains(p.Id)).ToList();
                return pages;
            }
        }
    }
}
