using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ClassLibraryLab4
{

    public class DBEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }

    }

    public class DbContext : System.Data.Entity.DbContext
    {
        public DbSet<DBEntity> Entities { get; set; }
    }

    public class DataAccess
    {
        public DbContext _context;
        public List<DBEntity> GetAllMessages()
        {
            return _context.Entities.ToList();
        }

        public DataAccess()
        {
            _context = new DbContext();
        }

        public DBEntity GetByID(int id)
        {
            return _context.Entities.FirstOrDefault(e => e.ID == id);
        }

        public List<DBEntity> GetByName(string name)
        {
            return _context.Entities.Where(e => e.Name == name).ToList();
        }

        public void Add(DBEntity entity)
        {
            _context.Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, string newMessage)
        {
            var entity = GetByID(id);
            if (entity != null)
            {
                entity.Message = newMessage;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            if (entity != null)
            {
                _context.Entities.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteAllData()
        {
            var allEntities = _context.Entities.ToList();
            _context.Entities.RemoveRange(allEntities);
            _context.SaveChanges();
        }
    }
}