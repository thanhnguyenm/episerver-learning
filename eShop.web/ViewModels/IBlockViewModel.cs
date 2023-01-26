using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class IBlockViewModel<T>
    {
        T CurrentBlock;
        string DebugId;
        string DebugCode;
    }
}