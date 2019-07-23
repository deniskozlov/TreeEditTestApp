using System.Collections.Generic;

namespace TreeEditTestApp.Models
{
    public class TreeItemModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<TreeItemModel> Nodes { get; set; }
    }
}