using Application.Interfaces;
using Application.Middlewares;
using Application.Services;
using Domain.Models;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_Commerce.Extensions
{
    public static class APIServiceCollectionExtensions
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();

            services.AddInfrastructureServices(configuration);

            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();

            //register automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddDefaultTokenProviders();

            // Add Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();

            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<ICategoryService, CategoryService>();


            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMessageService, MessageService>();

            // Configure JWT Authentication instead of cookies
            var key = Encoding.ASCII.GetBytes(configuration["ApiSettings:Secret"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromDays(7)
                };
            });

            // add authorizations ploicies 
            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));


            // Register the global exception handler
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token to access this API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.DocumentFilter<BearerSecuritySchemeFilter>();

                //c.AddServer(new OpenApiServer
                //{
                //    Url = "https://localhost:5001"
                //});
            });

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}