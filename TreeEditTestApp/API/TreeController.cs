
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using TreeEditTestApp.Contracts.Services;
using TreeEditTestApp.DataModel;
using TreeEditTestApp.Models;

namespace TreeEditTestApp.API
{
    public class TreeController : ApiController
    {
        private readonly ITreeItemService _treeItemService;

        public TreeController()
        {
            _treeItemService = DependencyResolver.Current.GetService<ITreeItemService>();
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<TreeItemModel> GetTreeItems(int levelFrom = 1)
        {
            var items = _treeItemService.GetWithLevel(levelFrom);
            var resultList = new List<TreeItemModel>();

            foreach (var treeItem in items)
            {
                resultList.Add(MapTreeItemModel(treeItem));
            }

            return resultList;
        }


        [System.Web.Http.HttpPost]
        public TreeItemModel AddTreeItem(long? parentId = null)
        {
            if (parentId.HasValue)
                return MapNewTreeItemModel(_treeItemService.AddTreeItemTo(parentId.Value));
            else
                return MapNewTreeItemModel(_treeItemService.AddTreeItem());

        }

        [System.Web.Http.HttpPut]
        public ResponseMessageResult MoveTreeItem(long id, long? parentId = null)
        {
            if (parentId.HasValue)
                _treeItemService.MoveTo(id, parentId.Value);
            else
                _treeItemService.MoveToTop(id);

            return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        [System.Web.Http.HttpDelete]
        public ResponseMessageResult RemoveTreeItem(long id)
        {
            _treeItemService.RemoveTreeItem(id);

            return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        private TreeItemModel MapTreeItemModel(TreeItem treeItem, string parentLevel = null)
        {
            var model = new TreeItemModel();
            model.Id = treeItem.Id.ToString();

            string currentLevel;
            if (!string.IsNullOrEmpty(parentLevel))
            {
                model.Title = $"node.{parentLevel}.{model.Id}";
                currentLevel = $"{parentLevel}.{treeItem.Id}";
            }
            else
            {
                model.Title = $"node.{model.Id}";
                currentLevel = $"{treeItem.Id}";
            }

            var nodes = new List<TreeItemModel>();

            foreach (var treeItemChild in treeItem.Children)
            {
                nodes.Add(MapTreeItemModel(treeItemChild, currentLevel));
            }

            model.Nodes = nodes;

            return model;
        }

        private TreeItemModel MapNewTreeItemModel(TreeItem treeItem)
        {
            var item = new TreeItemModel();
            item.Id = treeItem.Id.ToString();
            item.Title = GetFullTitle(treeItem);

            return item;
        }

        private string GetFullTitle(TreeItem treeItem)
        {
            if (treeItem == null)
                return string.Empty;


            var parentTitle = "node";

            if (treeItem.ParentId.HasValue)
            {
                var parent = _treeItemService.GetById(treeItem.ParentId.Value);
                if (parent != null)
                {
                    parentTitle = GetFullTitle(parent);
                }
            }

            return $"{parentTitle}.{treeItem.Id}";
        }
    }
}
