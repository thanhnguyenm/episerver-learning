//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace eShop.web.Business.Rendering
//{
//    /// <summary>
//    /// This attribute temporarilly comment out because I cannot know when it can be applied
//    /// </summary>
//    public class ManuallyBlockRenderingAttribute : ActionFilterAttribute, IExceptionFilter
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            using (var textWriter = new StringWriter())
//            {
//                var htmlHelper = CreateHtmlHelper(filterContext.Controller, textWriter);
//                var tag = filterContext.Controller.ControllerContext.ParentActionViewContext.ViewData["tag"].ToString();

//                RenderEpiserverContent(htmlHelper, epiServerInstance, tag);
//                var blockHtml = textWriter.ToString();
//            }
//        }

//        private HtmlHelper CreateHtmlHelper(ControllerBase controller, StringWriter textWriter)
//        {
//            var viewContext = new ViewContext(
//                 controller.ControllerContext,
//                 new WebFormView(controller.ControllerContext, "tmp"),
//                 controller.ViewData,
//                 controller.TempData,
//                 textWriter
//             );

//            return new HtmlHelper(viewContext, new ViewPage());
//        }

//        private static void RenderEpiserverContent(HtmlHelper html, IContentData content, string tag)
//        {
//            var templateResolver = ServiceLocator.Current.GetInstance<ITemplateResolver>();
//            var templateModel = templateResolver.Resolve(html.ViewContext.HttpContext, content.GetOriginalType(), content, TemplateTypeCategories.MvcPartial, tag);

//            var contentRenderer = ServiceLocator.Current.GetInstance<IContentRenderer>();
//            html.RenderContentData(content, true, templateModel, contentRenderer);
//        }

//        public void OnException(ExceptionContext filterContext)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}