using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Internal;
using Mediachase.Commerce.Catalog.Data;
using Mediachase.Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Seo
{
    public class ExtendUniqueSeoGenerator : UniqueSeoGenerator
    {
        private readonly IUrlSegmentGenerator urlSegmentGenerator;

        public ExtendUniqueSeoGenerator(IUrlSegmentGenerator urlSegmentGenerator) : base(urlSegmentGenerator)
        {
            this.urlSegmentGenerator = urlSegmentGenerator;
        }

        protected override string UriExtension
        {
            get
            {
                return ".html";
            }
        }

        public override string GenerateSeoUri(string name, string languageCode, bool includeRandomToken)
        {
            return includeRandomToken
              ? String.Format("{0}_{1}_{2}{3}", CommerceHelper.CleanUrlField(name), languageCode, GetRandomToken(), UriExtension)
              : String.Format("{0}_{1}{2}", CommerceHelper.CleanUrlField(name), languageCode, UriExtension);
        }

        public override string GenerateUriSegment(string name, bool includeRandomToken)
        {
            return includeRandomToken
              ? String.Format("{0}_{1}", urlSegmentGenerator.Create(name), GetRandomToken())
              : urlSegmentGenerator.Create(name);
        }

        protected override string GetRandomToken()
        {
            var chars = "abcdefghijklmnopqrstuvwzyz1234567890";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}