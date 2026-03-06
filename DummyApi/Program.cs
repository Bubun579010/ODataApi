using Data.Context;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Models.Entity;
using Repository.Implementation;
using Repository.Interface;
using Service.Implementation;
using Service.Interface;

namespace DummyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // OData Model
            var odatabuilder = new ODataConventionModelBuilder();
            odatabuilder.EntitySet<Product>("Product").EntityType.HasKey(p => p.ProductId);

            // Add services to the container.

            builder.Services.AddControllers()
                           .AddOData(options =>
                                  options.AddRouteComponents("odata", odatabuilder.GetEdmModel())
                                         .Select()
                                         .Filter()
                                         .OrderBy()
                                         .Expand()
                                         .Count()
                                         .SetMaxTop(100)
                                         //.EnableQueryFeatures(100)
                           );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                   policy.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod());
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

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

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
