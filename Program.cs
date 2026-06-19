using api_gestion_ecole.Data;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseNpgsql(connectionString ?? "");
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
builder.Services.AddScoped<IInscriptionRepository, InscriptionRepository>();
builder.Services.AddScoped<ICoursConcernerClasseRepository, CoursConcernerClasseRepository>();
builder.Services.AddScoped<IFraisConcernerClasseRepository, FraisConcernerClasseRepository>();
builder.Services.AddScoped<IPaiementRepository, PaiementRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();