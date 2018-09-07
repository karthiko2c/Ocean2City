using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.DotNet.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;   
using Ocean2City.Data.Interfaces;
using Ocean2City.Data;

namespace Ocean2City.WebApi
{
    /// <summary>
    /// Web api startup
    /// </summary>
    public class Startup
    {
		private const string Swaggersecret = "fe9f6272-8a49-464c-96cf-d342bc9c1bab";
       
        private const string DefaultCorsPolicyName = "localhost";

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"secrets/appsettings.secrets.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            RuntimeEnvironment = Configuration.GetSection("Environment")["ASPNETCore_Global"];
            DefaultThreshold = Configuration.GetSection("DefaultThreshold")["DefaultThreshold"];
        }

        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }
        public static string RuntimeEnvironment { get; set; }
        public static string DefaultThreshold { get; set; }
        public static bool UseMocks { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
			services.AddSingleton(Configuration);
           // services.AddDbContext<RSContext>(option => option.UseSqlServer(Configuration.GetConnectionString("RSConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Add all Transient dependencies
            services = BuildUnityContainer.RegisterAddTransient(services);

            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, p =>
                {
                    //todo: Get from confiuration
                    p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "Ocean2City API v1.0", Version = "v1.0" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                // add documentation to Swagger api
                var basePath = ApplicationEnvironment.ApplicationBasePath.ToLower();
                c.IncludeXmlComments(Path.Combine(basePath, "ocean2city.webapi.xml"));

                // Filter for file upload control to show on swagger ui
                c.OperationFilter<FileUploadOperation>();
            });
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
			{
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "the audience you want to validate",
                    ValidateIssuer = false,
                    //ValidIssuer = "the isser you want to validate",

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])),

                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(DefaultCorsPolicyName); //Enable CORS!
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //temporary simple authenticaion scheme to access swagger
            //TODO: remove swagger for non-local builds
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c  => {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1.0/swagger.json", "Versioned API v1.0");
                c.RoutePrefix = "api/swagger";
                c.DocExpansion("none");
            });
            
            app.UseAuthentication();
            app.UseMvc();
            app.UseDefaultFiles();
			app.UseStaticFiles();
        }


		private bool IsValid(string cookie){
			if (cookie == null || !cookie.Contains("|")) return false;
			bool retval = true;
			cookie = Uri.UnescapeDataString(cookie);
			var timestamp = cookie.Split("|")[0];
            var hash = cookie.Split("|")[1];

			var control = "";
			using(var sha256 = System.Security.Cryptography.SHA256.Create())  
            {  
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(timestamp+Swaggersecret));  
				control = BitConverter.ToString(hashedBytes);  
            }
            

			var cookietime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(timestamp));

			if (DateTimeOffset.UtcNow > cookietime.AddMinutes(15))
			{
				retval = false;
			}

			if(hash != control)
			{
				retval = false;
			}

			return retval;
		}
    }
}
