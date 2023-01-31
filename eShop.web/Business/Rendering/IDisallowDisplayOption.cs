using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.web.Business.Rendering
{
    public interface IDisallowDisplayOption
    {
        IEnumerable<string> DisabledDisplayOptions { get; }
    }
}
