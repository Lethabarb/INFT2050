using CDLibrary.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Repositories
{
    public class CdRepository
    {
        private TableQuery<CD> _cds;
        private SQLiteConnection _database;

        public CdRepository(DatabaseContext context)
        {
            _cds = context.Cds;
            _database = context.Database;
        }

        public List<CD> GetAll()
        {
            var res = _cds.ToList();
            return res;
        }


        public List<string> GetAllNames(string addition)
        {
            var list = _cds.ToList().ConvertAll<string>(x => x.Name);
            list.Add(addition);
            return list;
        }

        public void Create(CD t)
        {
            _database.Insert(t);
        }

        public List<CD> GetBy(Expression<Func<CD, bool>> pred)
        {
            return _cds.Where(pred).ToList();
        }

        public CD GetItem(int id)
        {
            return _cds.Where(t => t.Id == id).FirstOrDefault();
        }

        public void SaveItem(CD item)
        {
            CD t = GetItem(item.Id);
        }

        public int DeleteItem(Track item)
        {
            return _cds.Delete(i => i.Id == item.Id);
        }

    }
}
