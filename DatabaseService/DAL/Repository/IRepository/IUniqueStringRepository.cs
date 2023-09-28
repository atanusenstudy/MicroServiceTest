using DAL.Repository.IRepository;
using DatabaseService.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.DAL.Repository.IRepository
{
    /// <summary>
    /// Anything Which is implemented according to requirement of 
    /// specific class will be implemented here
    /// </summary>
    public interface IUniqueStringRepository : IRepository<UniqueString>
    {
        void Update(UniqueString contact);
    }
}
