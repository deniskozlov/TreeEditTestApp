
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeEditTestApp.Contracts.Data;
using TreeEditTestApp.Contracts.Services;
using TreeEditTestApp.DataModel;

namespace TreeEditTestApp.Services.TreeItems
{
    public class TreeItemService : ITreeItemService
    {
        private readonly IDataProvider<TreeItem> _treeDataProvider;

        public TreeItemService(IDataProvider<TreeItem> treeDataProvider)
        {
            _treeDataProvider = treeDataProvider;
        }

        public IEnumerable<TreeItem> GetWithLevel(int level)
        {
            return _treeDataProvider.Get(g => g.Level == level);
        }

        public TreeItem GetById(long id)
        {
            return _treeDataProvider.Get(id);
        }

        public TreeItem AddTreeItem()
        {
            var treeItem = new TreeItem();
            treeItem.Level = 1;
            _treeDataProvider.Add(treeItem);
            return _treeDataProvider.Get(treeItem.Id);
        }

        public TreeItem AddTreeItemTo(long parentId)
        {
            var parent = _treeDataProvider.Get(parentId);
            var treeItem = new TreeItem();
            treeItem.Level = parent.Level + 1;
            treeItem.ParentId = parentId;
            _treeDataProvider.Add(treeItem);
            return _treeDataProvider.Get(treeItem.Id);
        }

        public TreeItem MoveToTop(long treeItemId)
        {
            var treeItem = _treeDataProvider.Get(treeItemId);
            treeItem.ParentId = null;
            treeItem.Level = 1;

            return _treeDataProvider.Update(treeItem);
        }

        public TreeItem MoveTo(long treeItemId, long parentId)
        {
            var treeItem = _treeDataProvider.Get(treeItemId);
            var parentItem = _treeDataProvider.Get(parentId);
            treeItem.ParentId = parentItem.Id;

            treeItem.Level = parentItem.Level + 1;
            _treeDataProvider.Update(treeItem);
            return _treeDataProvider.Get(treeItem.Id);
        }

        public void RemoveTreeItem(long treeItemId)
        {
            var treeItem = _treeDataProvider.Get(treeItemId);
            _treeDataProvider.Delete(treeItem);
        }

    }
}
