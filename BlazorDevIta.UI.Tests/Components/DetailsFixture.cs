using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorDevIta.UI.Components;
using Bunit;
namespace BlazorDevIta.UI.Tests.Components
{
    [TestFixture]
    public class DetailsFixture
    {
        [Test]
        public void FirstView_ShoulRenderComponentCorrectly()
        {
            var ctx = new Bunit.TestContext();
            var model = new DetailsClassTest();
            model.Text ="test";

            var detail = ctx.RenderComponent<Details<DetailsClassTest>>(
                ps => ps.Add( p=> p.Item , model)
                .Add( x => x.Fields , i => $"<span>{i.Text}</span>")
                .Add( x=> x.HeaderPropertyName , nameof(model.Text))
                );

            detail.MarkupMatches(@$"
<h3>Details {model.Text}</h3>
<form >
  <span>{model.Text}</span>
  <button type=""button"" class=""btn btn-default"">Cancel</button>
  <button type=""submit"" class=""btn btn-primary"">Save</button>
</form>
");
        }

        [Test]
        public void ClickOnSave_ShouldRaiseOnSave()
        {
            var ctx = new Bunit.TestContext();
            var model = new DetailsClassTest();
            model.Text ="test";
            DetailsClassTest onSaveCallbackModel =null;
            var detail = ctx.RenderComponent<Details<DetailsClassTest>>(
                ps => ps.Add(p => p.Item, model)
                .Add(x => x.Fields, i => $"<span>{i.Text}</span>")
                .Add(x => x.HeaderPropertyName, nameof(model.Text))
                .Add( x => x.OnSave, (item) => onSaveCallbackModel = item)
                );

            var saveButton = detail.Find(".btn-primary");
            saveButton.Click();

            Assert.AreEqual(onSaveCallbackModel.Text, model.Text);
        }

        private class DetailsClassTest
        {
            [Required]
            public string Text { get; set; }
        }
    }
}
