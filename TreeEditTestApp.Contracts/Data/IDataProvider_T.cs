using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TreeEditTestApp.Contracts.Data
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(long id);
        T Add(T item);
        T Update(T item);
        void Delete(T item);
    }
}
