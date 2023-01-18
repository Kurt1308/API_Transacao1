using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Servico
{
    public interface IServicoBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);

    }
}
