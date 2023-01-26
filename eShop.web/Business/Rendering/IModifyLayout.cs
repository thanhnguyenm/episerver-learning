using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.web.Business.Rendering
{
    /// <summary>
    /// Defines a method which may be invoked by PageContextActionFilter allowing controllers
    /// to modify common layout properties of the view model.
    /// </summary>
    public interface IModifyLayout
    {
        void ModifyLayout(LayoutViewModel layoutModel);
    }
}
