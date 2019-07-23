
using System.Collections.Generic;

namespace TreeEditTestApp.DataModel
{
    public class TreeItem
    {
        public virtual long Id { get; set; }
        public virtual long? ParentId { get; set; }
        public virtual int Level { get; set; }
        public virtual ICollection<TreeItem> Children { get; set; } = new List<TreeItem>();
    }
}
