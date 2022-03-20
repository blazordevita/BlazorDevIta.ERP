using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorDevIta.ERP.Shared;
using BlazorDevIta.UI.Pages;
using BlazorDevIta.UI.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace BlazorDevIta.UI.Tests.Pages
{
    [TestFixture]
    public class FetchDataFixtures
    {
        private Mock<IDataServices> service;

        [SetUp]
        public void SetUp()
        {
            service = new Mock<IDataServices>();
        }

        [Test]
        public void FetchData_DataIsNull_ShouldViewLoading()
        {
            var ctx = new Bunit.TestContext();
            ctx.Services.AddSingleton<IDataServices>(service.Object);

            service.Setup(o => o.GetWeatherForecastsAsync())
                .ReturnsAsync(() => null);

            var component = ctx.RenderComponent<FetchData>();

            component.MarkupMatches($@"{headerComponent}
                                    <p><em>Loading...</em></p>");
        }

        [Test]
        public void FetchData_DataDoesNotNull_ShouldViewLoading()
        {
            var ctx = new Bunit.TestContext();
            ctx.Services.AddSingleton<IDataServices>(service.Object);
            var data = new WeatherForecast[]
            {
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "test1",
                    TemperatureC = 10
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(-1),
                    Summary = "test2",
                    TemperatureC = 20
                }
            };

            service.Setup(o => o.GetWeatherForecastsAsync())
                .ReturnsAsync(data);

            var component = ctx.RenderComponent<FetchData>();

            var expectedTable = $@"<table class=""table"">
                <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
               {string.Join(System.Environment.NewLine, data.Select(o => $@"<tr>
                    <td>{o.Date.ToShortDateString()}</td>
                    <td>{o.TemperatureC}</td>
                    <td>{o.TemperatureF}</td>
                    <td>{o.Summary}</td>
                </tr>"))}
        </tbody>
    </table>";

            component.MarkupMatches($@"{headerComponent}
                                    {expectedTable}");
        }

        [Test]
        public void FetchData_DataWithDelay_ShouldViewLoading()
        {
            var ctx = new Bunit.TestContext();
            ctx.Services.AddSingleton<IDataServices>(service.Object);
            var data = new WeatherForecast[]
            {
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "test1",
                    TemperatureC = 10
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(-1),
                    Summary = "test2",
                    TemperatureC = 20
                }
            };

            service.Setup(o => o.GetWeatherForecastsAsync())
                .Returns(async () =>
                {
                    await Task.Delay(1000);
                    return data;
                });


            var component = ctx.RenderComponent<FetchData>();
            component.MarkupMatches($@"{headerComponent}
                                    <p><em>Loading...</em></p>");

            component.WaitForElement("table", TimeSpan.FromSeconds(2));

            var expectedTable = $@"<table class=""table"">
                <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
               {string.Join(System.Environment.NewLine, data.Select(o => $@"<tr>
                    <td>{o.Date.ToShortDateString()}</td>
                    <td>{o.TemperatureC}</td>
                    <td>{o.TemperatureF}</td>
                    <td>{o.Summary}</td>
                </tr>"))}
        </tbody>
    </table>";

            component.MarkupMatches($@"{headerComponent}
                                    {expectedTable}");
        }

        [Test]
        public void Service_ThrowExecption_ShouldViewErrorMessage()
        {
            var ctx = new Bunit.TestContext();
            ctx.Services.AddSingleton<IDataServices>(service.Object);

            service.Setup(o => o.GetWeatherForecastsAsync())
                .ThrowsAsync(new InvalidOperationException());

            var component = ctx.RenderComponent<FetchData>();

            component.MarkupMatches($@"{headerComponent}
                                    <p>dati non disponibili</p>");
        }

        private string headerComponent => @"<h1>Weather forecast</h1>
<p>This component demonstrates fetching data from the server.</p>";
    }
}