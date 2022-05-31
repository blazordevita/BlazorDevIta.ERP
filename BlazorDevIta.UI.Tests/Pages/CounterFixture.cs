using BlazorDevIta.UI.Pages;
using Bunit;

namespace BlazorDevIta.UI.Tests.Pages
{
    [TestFixture]
    public class CounterFixture
    {
        [Test]
        public void FirstView_ShouldViewCounterWithZero()
        {
            var ctx = new Bunit.TestContext();

            var counter = ctx.RenderComponent<Counter>();

            counter.MarkupMatches(@"
<h1>Counter</h1>
<p role=""status"">Current count: 0</p>
<button class=""btn btn-primary"" >Click me</button>
");
        }

        [Test]
        public void ClickButton_ShouldViewCounterWithOne()
        {
            var ctx = new Bunit.TestContext();

            var counter = ctx.RenderComponent<Counter>();

            var button = counter.Find(".btn-primary");
            button.Click();

            counter.MarkupMatches(@"
<h1>Counter</h1>
<p role=""status"">Current count: 1</p>
<button class=""btn btn-primary"" >Click me</button>
");
        }
    }
}
