namespace GoogleAuthService
{
  using Microsoft.AspNetCore.Authentication.Google;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.OpenApi.Models;

  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var configuration = builder.Configuration;

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      //builder.Services.AddAuthentication().AddGoogle(googleOptions =>
      //{
      //  googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
      //  googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
      //});
      //builder.Services.AddAuthentication().AddGoogle(googleOptions =>
      //{
      //  googleOptions.ClientId = "69829818979-dd0aicu1rs7bpvmt0hj0d6j44vnnc4ee.apps.googleusercontent.com";
      //  googleOptions.ClientSecret = "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";
      //});

      //builder.Services
      //  .AddAuthentication(o =>
      //  {
      //    // This forces challenge results to be handled by Google OpenID Handler, so there's no
      //    // need to add an AccountController that emits challenges for Login.
      //    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
      //    // This forces forbid results to be handled by Google OpenID Handler, which checks if
      //    // extra scopes are required and does automatic incremental auth.
      //    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
      //    // Default scheme that will handle everything else.
      //    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
      //    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      //  })
      //  .AddCookie()
      //  .AddGoogleOpenIdConnect(options =>
      //  {
      //    options.ClientId = "69829818979-dd0aicu1rs7bpvmt0hj0d6j44vnnc4ee.apps.googleusercontent.com";
      //    options.ClientSecret = "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";
      //  });

      //builder.Services.AddIdentity<IdentityUser, IdentityRole>();
      builder.Services
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
          //options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddGoogle(options =>
        {
          options.ClientId = "69829818979-dd0aicu1rs7bpvmt0hj0d6j44vnnc4ee.apps.googleusercontent.com";
          options.ClientSecret = "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";
        });


      builder.Services.AddSwaggerGen(option =>
      {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        //option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //{
        //  In = ParameterLocation.Header,
        //  Description = "Please enter a valid token",
        //  Name = "Authorization",
        //  Type = SecuritySchemeType.Http,
        //  BearerFormat = "JWT",
        //  Scheme = "Bearer"
        //});
        //option.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type=ReferenceType.SecurityScheme,
        //                Id="Bearer"
        //            }
        //        },
        //        new string[]{}
        //    }
        //});
      });

      var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
      builder.Services.AddCors(options =>
      {
        options.AddPolicy(
          name: MyAllowSpecificOrigins,
          policy =>
          {
            policy.WithOrigins(
              "http://localhost:3000",
              "https://localhost:7165"
            );
          });
      });

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseCors(MyAllowSpecificOrigins);
      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}