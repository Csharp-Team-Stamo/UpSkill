﻿namespace UpSkill.ClientSide
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Authentication;
    using Authentication.Services;
    using Authentication.Services.Contracts;
    using Blazored.Toast;
    using Infrastructure.Services;
    using Infrastructure.Services.Contracts;

    public class Program
	{
        public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => {
                var client = new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApi:URL"]) };
                //client.DefaultRequestHeaders.Add("Origin", "https://upskillclientside.azurewebsites.net");
                return client;
            });

            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

            builder.Services.AddOidcAuthentication(options =>
			{
				// Configure your authentication provider options here.
				// For more information, see https://aka.ms/blazor-standalone-auth
				builder.Configuration.Bind("Local", options.ProviderOptions);
			});

            // Custom Services
            builder.Services.AddScoped<AuthenticationStateProvider, UpSkillAuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient<IEmployeesService, EmployeesService>();
            builder.Services.AddTransient<ICoachesService, CoachesService>();
            builder.Services.AddTransient<ICoursesService, CoursesService>();
            builder.Services.AddTransient<IDashboardService, DashboardService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
		}
	}
}
