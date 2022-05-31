using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorDevIta.UI.Components;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorDevIta.UI.Tests.Components
{
    [TestFixture]
    public class TextBoxFixture
    {
        [Test]
        public void ValueDoesNotNull_ShouldViewValue()
        {
            var ctx = new Bunit.TestContext();
            var id = Guid.NewGuid().ToString();
            var value = "test";
            var label = "label";

            Action<string> valueChanged = (s) => value = s;

            var textBox = ctx.RenderComponent<TextBox>( ps=>
                ps.AddCascadingValue(new EditContext(value))
                .Add( p=> p.Id, id)
                .Add( p => p.Label, label)
                .Add( p => p.Value, value)
                .Add( p=> p.ValueChanged, valueChanged)
                .Add( p => p.ValueExpression, () => value)
            );

            textBox.MarkupMatches(@$"
<div class=""form-group"">
  <label for=""{id}"">{label}</label>
  <input id=""{id}"" tvalue=""string"" class=""form-control valid"" value=""{value}""  >
</div>
");
        }

        [Test]
        public void ChangeValue_ShouldViewNewValueAndChangeParameter()
        {
            var ctx = new Bunit.TestContext();
            var id = Guid.NewGuid().ToString();
            var value = "test";
            var label = "label";

            Action<string> valueChanged = (s) => value = s;

            var textBox = ctx.RenderComponent<TextBox>(ps =>
                ps.AddCascadingValue(new EditContext(value))
                .Add(p => p.Id, id)
                .Add(p => p.Label, label)
                .Add(p => p.Value, value)
                .Add(p => p.ValueChanged, valueChanged)
                .Add(p => p.ValueExpression, () => value)
            );

            var input = textBox.Find(".form-control");
            
            var newValue="new value";
            input.Change(newValue);

            textBox.MarkupMatches(@$"
<div class=""form-group"">
  <label for=""{id}"">{label}</label>
  <input id=""{id}"" tvalue=""string"" class=""form-control modified valid"" value=""{newValue}""  >
</div>
");
            Assert.AreEqual(value, newValue);
        }

        [Test]
        public void ChangeValueWithEmptyValueEditContextInvalid_ShouldViewErrorValidMessage()
        {
            var ctx = new Bunit.TestContext();
            var id = Guid.NewGuid().ToString();
            var label = "label";
            var model = new TestModel();
            model.Text = "test";
            var editContext = new EditContext(model);

            Action<string> valueChanged = (s) => model.Text = s;

            var textBox = ctx.RenderComponent<TextBox>(ps =>
                ps.AddCascadingValue(editContext)
                .Add(p => p.Id, id)
                .Add(p => p.Label, label)
                .Add(p => p.Value, model.Text)
                .Add(p => p.ValueChanged, valueChanged)
                .Add(p => p.ValueExpression, () => model.Text)
            );

            editContext.EnableDataAnnotationsValidation();

            var input = textBox.Find(".form-control");
            
            var newValue = "";
            input.Change(newValue);

            textBox.MarkupMatches(@$"
<div class=""form-group"">
  <label for=""{id}"">label</label>
  <input id=""{id}"" tvalue=""string"" aria-invalid=""true"" class=""form-control modified invalid"" value=""{newValue}""  >
  <div class=""validation-message"">The Text field is required.</div>
</div>
");
            Assert.AreEqual(model.Text, newValue);
        }

        private class TestModel
        {
            [Required]
            public string Text { get; set; }
        }
    }
}
