using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WithMediatR.Components.Pages.Forecasts
{
    public class ListAll
    {
        public class Query : IRequest<Model>
        {
            public DateTime StartDate { get; set; }
        }

        public class Model
        {
            public IEnumerable<WeatherForecast> Forecasts { get; set; } = new List<WeatherForecast>();

            public class WeatherForecast
            {
                public DateTime Date { get; set; }
                public int TemperatureC { get; set; }
                public int TemperatureF { get; set; }
                public string Summary { get; set; }
            }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private static string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var rng = new Random();
                var forecasts = Enumerable.Range(1, 5).Select(index => new Model.WeatherForecast
                {
                    Date = request.StartDate.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });

                return new Model { Forecasts = forecasts };
            }
        }
    }
}
