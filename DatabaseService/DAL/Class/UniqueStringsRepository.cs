using DAL.Repository;
using DatabaseService.DAL.Data;
using DatabaseService.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository.IRepository;
using DatabaseService.Core.Model;

namespace DatabaseService.DAL.Class
{
    // We are implementing IContactRepository but it will show error because
    // we have implemented this inside Repository.
    // Thats why whe are inheriting Repository<Contact>
    public class UniqueStringsRepository : Repository<UniqueString>, IUniqueStringRepository
    {
        private ApplicationDbContext _db;
        // We nee to pass ApplicationDbContext because
        // Repository<T> want's this
        public UniqueStringsRepository(ApplicationDbContext db ):base(db) 
        {
            _db = db;
        }

        public void Update(UniqueString uniqueString)
        {
            _db.UniqueStrings.Update(uniqueString);
        }
    }
}
