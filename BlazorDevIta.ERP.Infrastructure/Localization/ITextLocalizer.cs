using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDevIta.ERP.Infrastructure.Localization
{
    public interface ITextLocalizer
    {
        string Localize(string value);
    }
}
