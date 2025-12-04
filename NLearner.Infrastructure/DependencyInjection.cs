using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLearner.Application.Abstractions;
using NLearner.Domain.Entities;
using NLearner.Infrastructure.Persistence;
using NLearner.Infrastructure.Service;
using NLearner.Infrastructure.Services;

namespace NLearner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration cfg) {
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(cfg.GetConnectionString("DefaultConnectionString")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDeckService, DeckService>();
            services.AddScoped<ISidebarService, SidebarService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
