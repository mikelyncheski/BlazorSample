using BlazorSample.Client.Shared;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace BlazorSample.Client
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IDataLayer, DataLayer>(); 
		}


		public void Configure(IComponentsApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
