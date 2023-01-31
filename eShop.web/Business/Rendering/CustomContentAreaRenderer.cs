using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using System.Web.Mvc;

namespace eShop.web.Business.Rendering
{
    /// <summary>
    /// Extends the default <see cref="ContentAreaRenderer"/> to apply custom CSS classes to each <see cref="ContentFragment"/>.
    /// </summary>
    /// [ServiceConfiguration(typeof(CustomContentAreaRenderer), Lifecycle = ServiceInstanceScope.Unique)]=> used this if we wanna DI it without AddTransient
    public class CustomContentAreaRenderer : ContentAreaRenderer
    {
        protected override string GetContentAreaItemTemplateTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            var templateTag = base.GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);

            if (!string.IsNullOrWhiteSpace(templateTag))
                return templateTag;

            var defaultDisplayOption = contentAreaItem.GetContent() as IDefaultDisplayOption;

            return defaultDisplayOption?.DefaultDisplayOption.ToString() ?? string.Empty;
        }

        protected override string GetContentAreaItemCssClass(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            var baseItemClass = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);

            var tag = GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);
            return $"{baseItemClass} {GetTypeSpecificCssClasses(contentAreaItem, ContentRepository)} {GetCssClassForTag(tag)} {tag}";
        }

        //protected override void RenderContentAreaItem(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem, string templateTag, string htmlTag, string cssClass)
        //{
            
        //}

        //protected override bool ShouldRenderWrappingElement(HtmlHelper htmlHelper)
        //{
            
        //}

        /// <summary>
        /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
        /// </summary>
        /// <param name="tagName">Any tag name available, see <see cref="Global.ContentAreaTags"/></param>
        private static string GetCssClassForTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return "";
            }
            switch (tagName.ToLower())
            {
                case "full":
                    return "col-md-12";
                case "wide":
                    return "col-md-8";
                case "half":
                    return "col-md-6";
                case "narrow":
                    return "col-md-4";
                case "quarter":
                    return "col-md-3";
                default:
                    return string.Empty;
            }
        }

        private static string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem, IContentRepository contentRepository)
        {
            var content = contentAreaItem.GetContent();
            var cssClass = content == null ? string.Empty : content.GetOriginalType().Name.ToLowerInvariant();

            var customClassContent = content as ICustomCssInContentArea;
            if (customClassContent != null && !string.IsNullOrWhiteSpace(customClassContent.ContentAreaCssClass))
            {
                cssClass += string.Format(" {0}", customClassContent.ContentAreaCssClass);
            }

            return cssClass;
        }
    }


}