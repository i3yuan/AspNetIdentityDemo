using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebIdentityDemoV3._1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebIdentityDemoV3._1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //这里需要注意的是 options.SignIn.RequireConfirmedAccount 设置项，缺省设置为true，
            //这种情况下，新注册的用户需要进行确认才能完成注册，如果没有安装邮件系统，这个步骤无法完成，所以这里改为false。
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) 
                .AddEntityFrameworkStores<ApplicationDbContext>();


            #region 配置说明详情：
            //配置用户 //不支持正则表达式

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.User = new UserOptions
            //    {
            //        RequireUniqueEmail = true, //要求Email唯一
            //        AllowedUserNameCharacters = "abcdefgABCDEFG" //允许的用户名字符，默认是 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
            //    };
            //});

            //配置密码

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.Password = new PasswordOptions
            //    {
            //        RequireDigit = true, //要求有数字介于0-9 之间,  默认true
            //        RequiredLength = 6, //要求密码最小长度，   默认是 6 个字符
            //        RequireLowercase = true, //要求小写字母,  默认true
            //        RequireNonAlphanumeric = false, //要求特殊字符,  默认true
            //        RequiredUniqueChars = 0, //要求需要密码中的非重复字符数,  默认1
            //        RequireUppercase = false //要求大写字母 ，默认true
            //    };
            //});
            //锁定账户

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.Lockout = new LockoutOptions
            //    {
            //        AllowedForNewUsers = true, // 新用户锁定账户, 默认true
            //        DefaultLockoutTimeSpan = TimeSpan.FromHours(1), //锁定时长，默认是 5 分钟
            //        MaxFailedAccessAttempts = 3 //登录错误最大尝试次数，默认 5 次
            //    };
            //});
            //数据库存储 (如果不设置，主键则是 max 的字符串长度。)

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.Stores = new StoreOptions
            //    {
            //        MaxLengthForKeys = 128, // 主键的最大长度
            //        ProtectPersonalData = true //保护用户数据，要求实现 IProtectedUserStore 接口
            //    };
            //});
            //令牌配置

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.Tokens = new TokenOptions
            //    {
            //        AuthenticatorTokenProvider = "MyAuthenticatorTokenProvider", //用于使用验证器验证双重登录的。
            //        ChangeEmailTokenProvider = "MyChangeEmailTokenProvider", //用于生成电子邮件更改确认电子邮件中使用的令牌的。
            //        ChangePhoneNumberTokenProvider = "MyChangePhoneNumberTokenProvider", //用于生成更改电话号码时使用的令牌的。
            //        EmailConfirmationTokenProvider = "MyEmailConfirmationTokenProvider", //用于生成帐户确认电子邮件中使用的令牌的令牌提供程序。
            //        PasswordResetTokenProvider = "MyPasswordResetTokenProvider", //用于生成密码重置电子邮件中使用的令牌
            //        ProviderMap = new Dictionary<string, TokenProviderDescriptor>(),  //用作提供程序名称的密钥构造 用户令牌提供程序 。
            //        AuthenticatorIssuer = "Identity", //认证的消费者      
            //    };
            //});
            //声明配置

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.ClaimsIdentity = new ClaimsIdentityOptions
            //    {
            //        RoleClaimType = "IdentityRole", // 用于角色声明的声明类型。
            //        UserIdClaimType = "IdentityId", // 用于用户标识符声明的声明类型。
            //        SecurityStampClaimType = "SecurityStamp", //用于安全戳声明的声明类型。
            //        UserNameClaimType = "IdentityName" //用于用户名声明的声明类型。
            //    };
            //});
            //登录认证配置 (在登录的时候，如果手机号或邮箱没有激活/确认，则无法登录。)

            //services.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.SignIn = new SignInOptions
            //    {
            //        RequireConfirmedEmail = true, //要求激活邮箱., 默认false
            //        RequireConfirmedPhoneNumber = true //要求激活手机号才能登录，默认false
            //    };
            //});
            //cooke设置

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
            //Password Hasher 选项设置  

            //services.Configure<PasswordHasherOptions>(option =>
            //{
            //    option.IterationCount = 12000; //使用 PBKDF2 对密码进行哈希处理时使用的迭代次数。
            //});
            //全局要求对所有用户进行身份验证

            //services.AddAuthorization(options =>
            //{
            //   options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //      .RequireAuthenticatedUser()
            //      .Build();
            //});


            #endregion


            #region 可以改成这样配置

            services.Configure<IdentityOptions>(options =>
            {
                // 配置用户 //不支持正则表达式
                //options.User = new UserOptions
                //{
                //    RequireUniqueEmail = true, //要求Email唯一
                //    AllowedUserNameCharacters = "abcdefgABCDEFG" //允许的用户名字符，默认是 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
                //};

                // 配置密码 
                options.Password = new PasswordOptions
                {
                    RequireDigit = false, //要求有数字介于0-9 之间,  默认true
                    RequiredLength = 6, //要求密码最小长度，   默认是 6 个字符
                    RequireLowercase = false, //要求小写字母,  默认true
                    RequireNonAlphanumeric = false, //要求特殊字符,  默认true
                    RequiredUniqueChars = 0, //要求需要密码中的非重复字符数,  默认1
                    RequireUppercase = false //要求大写字母 ，默认true
                };
                //  锁定账户

                //options.Lockout = new LockoutOptions
                //{
                //    AllowedForNewUsers = true, // 新用户锁定账户, 默认true
                //    DefaultLockoutTimeSpan = TimeSpan.FromHours(1), //锁定时长，默认是 5 分钟
                //    MaxFailedAccessAttempts = 3 //登录错误最大尝试次数，默认 5 次
                //};

                //  数据库存储(如果不设置，主键则是 max 的字符串长度。)

                //options.Stores = new StoreOptions
                //{
                //    MaxLengthForKeys = 128, // 主键的最大长度
                //    ProtectPersonalData = true //保护用户数据，要求实现 IProtectedUserStore 接口
                //};

                // 令牌配置

                //options.Tokens = new TokenOptions
                //{
                //    AuthenticatorTokenProvider = "MyAuthenticatorTokenProvider", //用于使用验证器验证双重登录的。
                //    ChangeEmailTokenProvider = "MyChangeEmailTokenProvider", //用于生成电子邮件更改确认电子邮件中使用的令牌的。
                //    ChangePhoneNumberTokenProvider = "MyChangePhoneNumberTokenProvider", //用于生成更改电话号码时使用的令牌的。
                //    EmailConfirmationTokenProvider = "MyEmailConfirmationTokenProvider", //用于生成帐户确认电子邮件中使用的令牌的令牌提供程序。
                //    PasswordResetTokenProvider = "MyPasswordResetTokenProvider", //用于生成密码重置电子邮件中使用的令牌
                //    ProviderMap = new Dictionary<string, TokenProviderDescriptor>(),  //用作提供程序名称的密钥构造 用户令牌提供程序 。
                //    AuthenticatorIssuer = "Identity", //认证的消费者      
                //};

                // 声明配置

                //options.ClaimsIdentity = new ClaimsIdentityOptions
                //{
                //    RoleClaimType = "IdentityRole", // 用于角色声明的声明类型。
                //    UserIdClaimType = "IdentityId", // 用于用户标识符声明的声明类型。
                //    SecurityStampClaimType = "SecurityStamp", //用于安全戳声明的声明类型。
                //    UserNameClaimType = "IdentityName" //用于用户名声明的声明类型。
                //};

                //登录认证配置(在登录的时候，如果手机号或邮箱没有激活 / 确认，则无法登录。)

                //options.SignIn = new SignInOptions
                //{
                //    RequireConfirmedEmail = true, //要求激活邮箱., 默认false
                //    RequireConfirmedPhoneNumber = true //要求激活手机号才能登录，默认false
                //};

            });
            //cooke设置

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
            //Password Hasher 选项设置  

            //services.Configure<PasswordHasherOptions>(option =>
            //{
            //    option.IterationCount = 12000; //使用 PBKDF2 对密码进行哈希处理时使用的迭代次数。
            //});
            //全局要求对所有用户进行身份验证

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //       .RequireAuthenticatedUser()
            //       .Build();
            //});

            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
