using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Persistence.Contexts;
using VotingSistem.Domain.Persistence.Repositories;
using VotingSistem.Domain.Services;
using VotingSistem.Exceptions;
using VotingSistem.Persistence.Repositories;
using VotingSistem.Services;

namespace VotingSistem
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

            services.AddControllers();

            //Database

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Dependency Injection Configuration
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IVoteService, VoteService>();


            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VotingSistem", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VotingSistem v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
