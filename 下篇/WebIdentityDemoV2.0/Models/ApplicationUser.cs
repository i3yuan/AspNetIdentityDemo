using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebIdentityDemoV2._0.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserTrueName { get; set; }

    }
}
