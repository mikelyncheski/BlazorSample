using BlazorSample.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;

namespace BlazorSample.Server.Controllers
{
	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
		private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		[HttpGet("[action]")]
		public IEnumerable<WeatherForecast> WeatherForecasts()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			});
		}


		[HttpGet("[action]")]
		public IEnumerable<ActivityFormattedDto> Activities()
		{
			//https://github.com/timheuer/strava-net  Later
			var messageStream = System.IO.File.OpenText(@"./TestData/Activities.json");
			var contents = messageStream.ReadToEnd();
			messageStream.Close();

			var list = JsonConvert.DeserializeObject<List<StravaActivity>>(contents);

			return list.Select(a => new ActivityFormattedDto
			{
				Name = a.Name,
				Distance = a.DistanceInMiles.ToString() + " mi",
				StartDate = a.StartDate.ToShortDateString(),
				Elevation = a.AltitudeInFeet.ToString() + " ft"
			});
		}
	}


	public class StravaActivity
	{
		public string Name { get; set; }

		[JsonProperty(PropertyName = "start_date_local")]
		public DateTimeOffset StartDateLocal { get; set; }

		public decimal Distance { get; set; }


		[JsonProperty(PropertyName = "total_elevation_gain")]
		public decimal TotalElevationGain { get; set; }

		public decimal DistanceInMiles => Math.Round(Distance * 0.000621M, 2);

		public DateTime StartDate => StartDateLocal.DateTime;
		public int AltitudeInFeet => Decimal.ToInt32(TotalElevationGain * 3.28084M);
	}
}
