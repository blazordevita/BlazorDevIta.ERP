using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorDevIta.UI.Components
{
    public partial class LanguageSelector
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        CultureInfo[] cultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("it"),
            new CultureInfo("fr")
        };

        CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var js = (IJSInProcessRuntime)JSRuntime;
                    js.InvokeVoid("blazorLanguage.set", value.Name);
                    NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
                }
            }
        }

    }
}
