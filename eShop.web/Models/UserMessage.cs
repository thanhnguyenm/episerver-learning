using EPiServer.Core;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models
{
    [ContentType(DisplayName = "Message",
        GUID = "85B7A37A-F3AF-4807-B176-70FB07A887DC",
        Description = "")]
    public class UserMessage : BasicContent
    {
        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Message { get; set; }
    }
}