using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataModel.Enums
{
    public enum ProjectStatus : short
    {
        /// <summary>
        /// 待启动
        /// </summary>
        ToBeActivated = 0,
        /// <summary>
        /// 已启动
        /// </summary>
        Activated = 1,
        /// <summary>
        /// 施工中
        /// </summary>
        Working = 2,
        /// <summary>
        /// 返工中
        /// </summary>
        Reworking = 3,
        /// <summary>
        /// 已验收
        /// </summary>
        Approved = 4
    }
}
