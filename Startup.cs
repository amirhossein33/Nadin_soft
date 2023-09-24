using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using myproject.Data;
using myproject.Repository;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;


namespace myproject
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
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<ProductContext>(options =>
          options.UseNpgsql(Configuration.GetConnectionString("ProductConnectionString")));
            services.AddIdentity<ApplicationUser, UserRole>().AddEntityFrameworkStores<ProductContext>()
            .AddDefaultTokenProviders();
            services.AddAuthentication(option =>
               {
                   option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               }).AddJwtBearer(opt =>
               {
                   opt.SaveToken = true;
                   opt.RequireHttpsMetadata = false;
                   opt.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidIssuer = Configuration["JWT:ValidationIssuer"],
                       ValidAudience = Configuration["JWT:ValidationAudiance"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                   };
               });




            services.AddControllers();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSwaggerGen(conf =>
            {
                conf.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert Jwt token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                conf.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
             new OpenApiSecurityScheme{
            Reference = new OpenApiReference
           {
               Type = ReferenceType.SecurityScheme ,
               Id = "Bearer"
           }        

           } ,
              new String[] {}
            }

                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("v1/swagger.json", "My API V1");
           });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
