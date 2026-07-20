using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Hydra.Auth.Domain;
using Hydra.Infrastructure.Data;

namespace Hydra.Infrastructure.Security
{
    public static class DbContextStartup
    {
        public static void AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddIdentityCore<User>(o => o.SignIn.RequireConfirmedAccount = false)
                 .AddRoles<Role>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Configuration = new OpenIdConnectConfiguration();
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false, // on production make it true
                    ValidateAudience = false, // on production make it true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["Authentication:Schemes:Bearer:ValidAudiences"],
                    ValidIssuer = configuration["Authentication:Schemes:Bearer:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Schemes:Bearer:Secret"])),
                    ClockSkew = TimeSpan.Zero,

                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy.
                //options.FallbackPolicy = options.DefaultPolicy;
            });
            //services.AddAuthorizationBuilder()
            //.AddPolicy("admin_greetings", policy =>
            //    policy
            //.RequireRole("admin")
            //.RequireScope("greetings_api"));


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = configuration.GetValue<bool>("IdentitySettings:Password:RequireDigit");
                options.Password.RequireLowercase = configuration.GetValue<bool>("IdentitySettings:Password:RequireLowercase");
                options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("IdentitySettings:Password:RequireNonAlphanumeric");
                options.Password.RequireUppercase = configuration.GetValue<bool>("IdentitySettings:Password:RequireUppercase");
                options.Password.RequiredLength = configuration.GetValue<int>("IdentitySettings:Password:RequiredLength");
                options.Password.RequiredUniqueChars = configuration.GetValue<int>("IdentitySettings:Password:RequiredUniqueChars");

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(configuration.GetValue<int>("IdentitySettings:Lockout:DefaultLockoutTimeSpanMinutes"));
                options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("IdentitySettings:Lockout:MaxFailedAccessAttempts");
                options.Lockout.AllowedForNewUsers = configuration.GetValue<bool>("IdentitySettings:Lockout:AllowedForNewUsers");

                // User settings.
                options.User.AllowedUserNameCharacters = configuration["IdentitySettings:User:AllowedUserNameCharacters"];
                options.User.RequireUniqueEmail = configuration.GetValue<bool>("IdentitySettings:User:RequireUniqueEmail");

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("IdentitySettings:SignIn:RequireConfirmedEmail");
                options.SignIn.RequireConfirmedPhoneNumber = configuration.GetValue<bool>("IdentitySettings:SignIn:RequireConfirmedPhoneNumber");
            });

            // Hosting doesn't add IHttpContextAccessor by default
            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IUserValidator<User>, UserValidator<User>>();
            services.TryAddScoped<IPasswordValidator<User>, PasswordValidator<User>>();
            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<Role>, RoleValidator<Role>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<User>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();
            services.TryAddScoped<UserManager<User>>();
            services.TryAddScoped<SignInManager<User>>();
            services.TryAddScoped<RoleManager<Role>>();

        }
    }
}
