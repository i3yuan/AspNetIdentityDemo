using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIdentityDemoV3._1.Models
{
    public class ApplicationUser:IdentityUser<Guid>
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
