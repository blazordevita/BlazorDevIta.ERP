using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace BlazorDevIta.ERP.Infrastructure.Localization
{
    public class TextLocalizer : ITextLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public TextLocalizer(IStringLocalizerFactory factory, Type language)
        {
            _localizer = factory.Create(language);
        }

        public string Localize(string value)
        {
            //var ret = _localizer[value];
            var ret = _localizer.GetString(value);
            return ret;
        }
    }
}
