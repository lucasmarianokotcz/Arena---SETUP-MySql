using System.Collections.Generic;

namespace Intermediario.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(int id);
        List<T> Select();
    }
}
