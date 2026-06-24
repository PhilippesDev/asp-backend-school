using System.Text;
using api_gestion_ecole.Data;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Models;
using api_gestion_ecole.Repositories;
using api_gestion_ecole.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseNpgsql(connectionString ?? "");
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options=>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
}
).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultSignInScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options=> options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration["JWT:SigninKey"] ?? string.Empty))
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<IAnneeScolaireRepository, AnneeScolaireRepository>();
builder.Services.AddScoped<ICategorieFraisRepository, CategorieFraisRepository>();
builder.Services.AddScoped<IClasseRepository, ClasseRepository>();
builder.Services.AddScoped<ICoursRepository, CoursRepository>();
builder.Services.AddScoped<IEleveRepository, EleveRepository>();
builder.Services.AddScoped<IFraisRepository, FraisRepository>();
builder.Services.AddScoped<IOptionRepository, OptionRepository>();
builder.Services.AddScoped<IPeriodeRepository, PeriodeRepository>();
builder.Services.AddScoped<IEnseignantRepository, EnseignantRepository>();
builder.Services.AddScoped<IInscriptionRepository, InscriptionRepository>();
builder.Services.AddScoped<ICoursConcernerClasseRepository, CoursConcernerClasseRepository>();
builder.Services.AddScoped<IFraisConcernerClasseRepository, FraisConcernerClasseRepository>();
builder.Services.AddScoped<IPaiementRepository, PaiementRepository>();
builder.Services.AddScoped<ISemestreRepository, SemestreRepository>();
builder.Services.AddScoped<ICotationRepository, CotationRepository>();
builder.Services.AddScoped<IBulletinsRepository, BulletinsRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UploadImageService>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<TokenService>();


const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // L'URL du frontend React
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.Run();