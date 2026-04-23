using SystemCalculatorShip.Web.Components;
using SystemCalculatorShip.Web.Services;

namespace SystemCalculatorShip.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5332";
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });
            builder.Services.AddScoped<ServiceClient>(sp =>
                new ServiceClient(
                    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"),
                    sp.GetRequiredService<Microsoft.JSInterop.IJSRuntime>()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
