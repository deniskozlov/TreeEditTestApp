
using FluentNHibernate.Mapping;
using TreeEditTestApp.DataModel;

namespace TreeEditTestApp.Data.Mapping
{
    /// <summary>
    /// Mapping configuration for TreeItem class
    /// </summary>
    public class TreeItemMap : ClassMap<TreeItem>
    {
        public TreeItemMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.ParentId);
            Map(m => m.Level);
            HasMany(m => m.Children)
                .KeyColumn("ParentId")
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Not.LazyLoad();
            Table("TreeItems");
        }
    }
}
