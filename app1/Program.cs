using app1.Logger;
using app1.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.IO;
using AutoMapper;
using app1.MapperConfig;
using app1.Repository.RepositoryInterface;
using app1.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//adding key token to use it with configuration service
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// To configure swagger to use
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n"+
                      "Enter 'Bearer' [space] and then your token in the text input below."+
                      "\r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Magic Villa V1",
        Description = "API to manage Villa",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contactwebsite",
            Url = new Uri("https://contactwebsite.com")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });


    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Magic Villa V2",
        Description = "API to manage Villa",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contactwebsite",
            Url = new Uri("https://contactwebsite.com")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});



//Auto mapper service
builder.Services.AddAutoMapper(typeof(Mapper_1));
//Repository service
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<ILocalUsers, LocalUsers>();
//builder.Services.AddSingleton<ILoggers, Loggers>();
//builder.Services.AddSingleton<ILoggersV2, LoggersV2>();
//Add service patch to the application
//***************************************************************************************//
//configure service for Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x=>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters { 
    
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    ValidateIssuer = false,
    ValidateAudience = false
    };
});

//versioning service 
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
});

//configure service for multi versioning

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // to let route take a version automatically
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddControllers(option => { })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
//DbContext service
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(u =>
    {
        u.SwaggerEndpoint("/swagger/v1/swagger.json","app1_test_API_1");
        u.SwaggerEndpoint("/swagger/v2/swagger.json", "app1_test_API_2");
    });
}

app.UseHttpsRedirection();

//configure authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
