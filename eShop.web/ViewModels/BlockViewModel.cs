using EPiServer.Core;
using eShop.web.Helpers;
using eShop.web.Models.Pages;
using System.Linq;

namespace eShop.web.ViewModels
{
    public class BlockViewModel<T> : IBlockViewModel<T> where T : BlockData
    {
        public BlockViewModel(T currentBlock)  : this(currentBlock, string.Empty) 
        {
        }

        public BlockViewModel(T currentBlock, string displayOptionTag)
        {
            CurrentBlock = currentBlock;
            if (currentBlock is IContent)
            {
                CurrentBlockLink = ((IContent)currentBlock)?.ContentLink;
            }
        }

        internal string DisplayOptionTag { get; }

        public T CurrentBlock { get; }
        public ContentReference CurrentBlockLink { get; }

        public PageData CurrentPage { get; set; }
        public PageReference CurrentPageLink { get; set; }

        public string DebugId => $"{CurrentBlock.GetType()} {(CurrentBlock as IContent).ContentLink.ID} {DisplayOptionTag}";

        public string DebugCode => $"<div class=\"Red\">{DebugId}</div>";  
    }

    public static class BlockViewModel
    {
        /// <summary>
        /// Returns a PageViewModel of type <typeparam name="T"/>.
        /// </summary>
        /// <remarks>
        /// Convenience method for creating PageViewModels without having to specify the type as methods can use type inference while constructors cannot.
        /// </remarks>
        public static BlockViewModel<T> Create<T>(T block, PageData currentPage, PageReference currentPageLink) where T : BlockData
        {
            return new BlockViewModel<T>(block)
            {
                CurrentPage = currentPage,
                CurrentPageLink = currentPageLink
            };
        }
    }
}