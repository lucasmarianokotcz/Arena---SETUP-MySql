using System.Collections.Generic;

namespace Intermediario.Interfaces
{
    internal interface IRepositorySelectById<T> where T : class
    {
        List<T> SelectById(int id);
    }
}
