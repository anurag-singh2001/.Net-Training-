
using CollegeMgmtSystem.CosmoDB;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Service;
using StudentMgmtSys.Common;

namespace CollegeMgmtSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICosmoDBService, CosmoDBService>();
            builder.Services.AddScoped<ICollegeService, CollegeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();



            app.MapControllers();

            app.Run();
        }
    }
}
