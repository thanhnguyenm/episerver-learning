using EPiServer.Core;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class BaseFormBlockController<TBlockData> : BaseBlockController<TBlockData> where TBlockData : BlockData
    {
        protected virtual void SaveModelState(ContentReference blockLink, object formModel)
        {
            if(ViewData.ModelState != null && !ViewData.ModelState.IsValid)
            {
                TempData[StateKey(blockLink)] = ViewData.ModelState;
                TempData[StateKey(blockLink) + "_Model"] = formModel;
            }
        }

        protected virtual void LoadModelState(ContentReference blockLink, out object formModel)
        {
            var key = StateKey(blockLink);
            var keymodel = key + "_Model";
            var modelState = TempData[key] as ModelStateDictionary;
            formModel = null;
            if (modelState != null)
            {
                ViewData.ModelState.Merge(modelState);
                TempData.Remove(key);

                formModel = TempData[keymodel];
                TempData.Remove(keymodel);
            }
        }
        

        private static string StateKey(ContentReference blockLink)
        {
            return typeof(BaseFormBlockController<TBlockData>).FullName + $"_{blockLink.ID}";
        }
    }
}