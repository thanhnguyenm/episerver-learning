using System;
using System.Web.Mvc;

namespace eShop.web.ViewModels
{
    public class ConditionalLink : IDisposable
    {
        private readonly ViewContext _viewContext;
        private readonly bool _linked;
        private bool _disposed;

        public ConditionalLink(ViewContext viewContext, bool isLinked)
        {
            _viewContext = viewContext;
            _linked = isLinked;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (_linked)
            {
                _viewContext.Writer.Write("</a>");
            }
        }
    }
}