using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



using backend.DbContext;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbSettings>(  
            Configuration.GetSection(nameof(DbSettings)));  
  
            services.AddSingleton<IDbSettings>(sp =>  
            sp.GetRequiredService<IOptions<DbSettings>>().Value);  
  
            services.AddSingleton<UserOperation>(); 
            services.AddSingleton<ServiceOperation>(); 

            // JWT Authentication service
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;  

                options.TokenValidationParameters = new TokenValidationParameters    
                {    
                    ValidateIssuer = true,    
                    ValidateAudience = true,    
                    ValidateLifetime = true,    
                    ValidateIssuerSigningKey = true,    
                    ValidIssuer = Configuration["Jwt:Issuer"],    
                    ValidAudience = Configuration["Jwt:Audience"],    
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))    
                };    
            });   

            services.AddControllers();

            services.AddCors(); // Adding CORS functionality

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "SelfService", Version = "v1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(policy => policy // CORS policy
                .AllowAnyHeader()
                .AllowAnyMethod()                    
                .WithOrigins("http://localhost:8080")
                .AllowCredentials()
                .WithExposedHeaders("reg_code", "login_code", "jwt_token", "service_code")
                );

               // app.UseSwagger();
               // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SelfService v1"));

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseSession();

            // app.Use(async (context, next) =>
            // {
            //     var token = context.Session.GetString("Token");
            //     if (!string.IsNullOrEmpty(token))
            //     {
            //         context.Request.Headers.Add("Authorization", "Bearer " + token);
            //     }
            //     await next();
            // });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
