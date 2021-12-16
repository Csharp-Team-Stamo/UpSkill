namespace UpSkill.Api
{
    using System.Text;
    using Data;
    using Data.Common.Repositories;
    using Data.Models;
    using Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Services.Data;
    using Services.Data.Contracts;
    using UpSkill.Services.Data.Admin;
    using UpSkill.Services.Data.Admin.Contracts;

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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequiredUniqueChars = 0;

                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            var jwtSettings = Configuration.GetSection("JWTSettings");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
                };
            });



            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpSkillApi", Version = "v1" });
            });

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination"));
            });

            services.AddHttpClient();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                    .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                    .AddScoped<IDbQueryRunner, DbQueryRunner>();

            //Business logic services

            services.AddTransient<IAccountsService, AccountsService>()
                    .AddTransient<IAdminCategoryService, AdminCategoryService>()
                    .AddTransient<IAdminCoachService, AdminCoachService>()
                    .AddTransient<IAdminCompanyService, AdminCompanyService>()
                    .AddTransient<IAdminCourseService, AdminCourseService>()
                    .AddTransient<IAdminLanguageService, AdminLanguageService>()
                    .AddTransient<IAdminLectureService, AdminLectureService>()
                    .AddTransient<IApplicationUserService, ApplicationUserService>()
                    .AddTransient<ICategoriesService, CategoriesService>()
                    .AddTransient<ICoachesService, CoachesService>()
                    .AddTransient<ICoachSessionsService, CoachSessionsService>()
                    .AddTransient<ICompanyService, CompanyService>()
                    .AddTransient<ICoursesService, CoursesService>()
                    .AddTransient<IDashboardService, DashboardService>()
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<IEmployeesService, EmployeesService>()
                    .AddTransient<ILanguagesService, LanguagesService>()
                    .AddTransient<ILectureService, LectureService>()
                    .AddTransient<IOwnerService, OwnerService>()
                    .AddTransient<IStatisticsService, StatisticsService>()
                    .AddTransient<IimagesService, ImagesService>()
                    .AddTransient<ICloudinaryService, CloudinaryService>();

            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];
            });

            services.Configure<CalendlyOptions>(options =>
            {
                options.Token = Configuration["ExternalProviders:Calendly:Token"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpSkillApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();
            });
        }
    }
}
