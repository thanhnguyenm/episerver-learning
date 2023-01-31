using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace eShop.web.Business.Services
{
    public interface IFeedService
    {
        SyndicationFeed Generate(string title, string url, string description);
    }
}
