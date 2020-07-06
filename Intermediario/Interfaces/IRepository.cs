using System.Collections.Generic;

namespace Intermediario.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        List<T> Select();
    }
}
