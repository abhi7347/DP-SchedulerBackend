
using DPScheduler.BAL.Implementation;
using DPScheduler.BAL.Interface;
using DPScheduler.DAL.Implementation;
using DPScheduler.DAL.Interface;
using DPScheduler.DAL.Model;

namespace DPscheduler.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            builder.Services.AddSingleton<SchedulerDBContext>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IRepository, Repository>();
            builder.Services.AddSingleton<IService, Service>();

            builder.Services.AddSingleton<IEventOperationsRepo, EventOperationsRepo>();
            builder.Services.AddSingleton<IEventOperationsService, EventOperationsService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
