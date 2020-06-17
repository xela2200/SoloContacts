using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoloContacts.Library.Interfaces;
using SoloContacts.Library.Models;
using SoloContacts.Library.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SoloContacts.Library
{
   public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            

            services.AddSingleton<IContactService<Contact>, ContactService>();

            //services.AddSingleton<SubscriptionStore>();
            //services.AddSingleton<DashboardStore>();
            //services.AddSingleton<CollectibleStore>();
            //services.AddSingleton<RequestFeatureStore>();
            //services.AddSingleton<NotificationStore>();
            //services.AddSingleton<SupportTicketStore>();
            //services.AddSingleton<CollectibleImageStore>();
            //services.AddSingleton<ReportStore>();
            //services.AddTransient<UserStore>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.Password.RequireDigit = true;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 5;
            }).AddDefaultTokenProviders();


            //// Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();


            return services;
        }
    }
}
