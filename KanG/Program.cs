using System.Text;
using KanG.Data;

using KanG.Services;
using KanG.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000", "http://127.0.0.1:5173", "http://127.0.0.1:5174")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Register Application Services (Service Pattern)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<ITripPlanService, TripPlanService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPlaceImageService, PlaceImageService>();

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "KanGSuperSecretKeyForJwtTokenAuth2026KanchanaburiTravelApp!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "KanGServer";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "KanGClient";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

// Configure Swagger/OpenAPI with JWT Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.CustomSchemaIds(type => type.FullName);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "กรุณากรอก JWT Token เช่น: eyJhbGciOi..."
    });

    options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();

var fileTypeProvider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
fileTypeProvider.Mappings[".apk"] = "application/vnd.android.package-archive";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = fileTypeProvider
});

app.UseCors("AllowVueApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

// Seed initial demo notifications
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.SeedNotifications(context);
}

app.Run();
