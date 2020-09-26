using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Globalization;
using Blazor.Extensions.Storage;
using HomeAPP.WebAPPs.SPA.Blazor.WASM.Extensions;
using Microsoft.JSInterop;
using HomeAPP.WebAPPs.SPA.Blazor.WASM.Services.Repositories.Abstractions;

namespace HomeAPP.WebAPPs.SPA.Blazor.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddExternalServices()
                .AddServices()
                .AddHttpClient(builder.HostEnvironment.BaseAddress);

            var storageRepo = builder.Services.BuildServiceProvider().GetRequiredService<IStorageRepository>();
            var language = await storageRepo.GetItem<string>("lang");


            var culture = new CultureInfo(language);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await builder.Build().RunAsync();
        }
    }
}
