using DatabaseService.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    /// <summary>
    /// This will do all the database Operations rather than individual repository
    /// </summary>
    public interface IUnitOfWork
    {
        IUniqueStringRepository UniqueString { get; }
        void Save();
    }
}
