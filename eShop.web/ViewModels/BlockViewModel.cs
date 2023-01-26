using EPiServer.Core;

namespace eShop.web.ViewModels
{
    public class BlockViewModel<T> : IBlockViewModel<T> where T : BlockData
    {
        public BlockViewModel(T currentBlock, string displayOptionTag)
        {
            CurrentBlock = currentBlock;
        }

        internal string DisplayOptionTag { get; }

        public T CurrentBlock { get; }

        public string DebugId => $"{CurrentBlock.GetType()} {(CurrentBlock as IContent).ContentLink.ID} {DisplayOptionTag}";

        public string DebugCode => $"<div class=\"Red\">{DebugId}</div>";  
}
}