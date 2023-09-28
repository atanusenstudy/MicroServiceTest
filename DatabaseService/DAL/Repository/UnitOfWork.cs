using DAL.Repository.IRepository;
using DatabaseService.DAL.Class;
using DatabaseService.DAL.Data;
using DatabaseService.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.DAL.Repository
{
    /// <summary>
    /// This will do all the database Operations rather than individual repository
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUniqueStringRepository UniqueString { get;private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UniqueString = new UniqueStringsRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
