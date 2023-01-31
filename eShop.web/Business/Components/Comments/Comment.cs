using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Business.Components.Comments
{
    [ContentType(DisplayName = "Comment",
        GUID = "3fe41747-e7b2-42b4-b2ec-2dfe90faf3e8",
        Description = "")]
    public class Comment : BasicContent
    {
        public virtual XhtmlString UserComment { get; set; }

        public virtual string PostedBy { get; set; }

    }
}