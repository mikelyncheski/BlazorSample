using System;
using System.Collections.Generic;
using System.Linq;		// This is needed for the GetJsonAsync!  ***
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using BlazorSample.Shared;

namespace BlazorSample.Client.Shared
{
	public interface IDataLayer
	{
		Task<ActivityFormattedDto[]> FetchActivities();
	}


	public class DataLayer : IDataLayer
	{
		private readonly HttpClient httpClient;
		private const string baseUrl = "api/SampleData/";


		public DataLayer(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}


		public async Task<ActivityFormattedDto[]> FetchActivities()
		{
			var x = await httpClient.GetJsonAsync<ActivityFormattedDto[]>(baseUrl + "Activities");
			return x;
		}
	}
}
