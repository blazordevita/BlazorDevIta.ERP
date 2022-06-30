using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public  record OrderedQueryable<T>( bool Error, IQueryable<T> Value);
    
}
