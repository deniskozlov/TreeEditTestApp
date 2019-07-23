
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using TreeEditTestApp.Contracts.Data;
using TreeEditTestApp.DataModel;

namespace TreeEditTestApp.Data.DataProviders
{
    public class TreeItemsDataProvider : IDataProvider<TreeItem>
    {
        private readonly ISessionFactory _sessionFactory;

        public TreeItemsDataProvider(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IEnumerable<TreeItem> Get(Expression<Func<TreeItem, bool>> predicate)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<TreeItem>().Where(predicate).Take(1000000).AsEnumerable().ToList();
            }
        }

        public TreeItem Get(long id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<TreeItem>(id);
            }
        }

        public TreeItem Add(TreeItem item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(item);
                session.Flush();
            }

            return item;
        }

        public TreeItem Update(TreeItem item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                }
            }

            return item;
        }

        public void Delete(TreeItem item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }
    }
}
