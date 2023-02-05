using EPiServer.Commerce.SpecializedProperties;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [ContentType(GUID = "4545D6CE-6EFA-4E10-883A-50F5B0D08AD0")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageFile : CommerceImage
    {
        public virtual String Description { get; set; }


        [Editable(false)]
        [ImageDescriptor(Width = 128, Height = 128)]
        public override Blob LargeThumbnail { get; set; }


        [Editable(false)]
        [ImageDescriptor(Width = 64, Height = 64)]
        public override Blob Thumbnail { get; set; }
    }
}