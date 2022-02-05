using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api
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
            services.AddCors();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json " });
            });

            services.AddControllers();

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddTransient<ITodoRepository, TodoRepository>();

            services.AddTransient<TodoHandler, TodoHandler>();

            services
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = "https://securetoken.google.com/todos-8f6a4";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = "https://securetoken.google.com/todos-8f6a4",
                            ValidateAudience = true,
                            ValidAudience = "todos-8f6a4",
                            ValidateLifetime = true
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todos Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todos Api V1");
            });

            app.UseRouting();

            app.UseCors(x =>
                x.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
