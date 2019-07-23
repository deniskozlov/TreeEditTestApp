

using System.Collections.Generic;
using TreeEditTestApp.DataModel;

namespace TreeEditTestApp.Contracts.Services
{
    public interface ITreeItemService
    {
        IEnumerable<TreeItem> GetWithLevel(int level);
        TreeItem GetById(long id);
        TreeItem AddTreeItem();
        TreeItem AddTreeItemTo(long parentId);
        TreeItem MoveToTop(long treeItemId);
        TreeItem MoveTo(long treeItemId, long parentId);
        void RemoveTreeItem(long treeItemId);
    }
}
