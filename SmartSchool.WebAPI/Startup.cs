using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI
{
    public class Startup
    {
        /*
        Quando se inicializa uma startup ele injeta essa configuração (Iconfigutation) para podermos acessar o appsettings.json
        */
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Explicar para outras services (Controller e Swang) quem sera o banco de dados
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))//Tem que ir em appsettings.json e criar a configuração
            );
            
            //services.AddSingleton<IRepository, Repository>(); = Todas as requisições num mesma classe

            //services.AddTransient<IRepository,Repository>(); = 1 classe 1 requisição

            services.AddScoped<IRepository, Repository>(); //Requisição no mesmo repositório renova a instancia de classe, em outros repositório cria outra instancias de classe

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // vai procurar dentro da dlls quem está herdando de profiles.. Fazer mapeamentos de dtos e dominios(models)

            services.AddControllers()
                    //.AddNewtonsoftJson ignora looping infinito do disciplina, aluno, professor, disciplina, aluno, professor)
                    .AddNewtonsoftJson(
                        opt =>opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartSchool.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartSchool.WebAPI v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
