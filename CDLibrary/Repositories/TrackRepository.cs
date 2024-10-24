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
    public class TrackRepository
    {
        private TableQuery<Track> _tracks;
        private SQLiteConnection _database;

        public TrackRepository(DatabaseContext context)
        {
            _tracks = context.Tracks;
            _database = context.Database;
        }

        public List<Track> GetAll()
        {
            var res = _tracks.ToList();
            var cds = App.Cds.GetAll();
            foreach (var item in res)
            {
                try
                {
                    item.CD = cds.Where(c => c.Id == item.CdId).First();

                }
                catch (Exception ex)
                {
                    item.CD = new CD()
                    {
                        Name = "No Cd",
                        Color = "#FFFFFF",
                        Index = -1
                    };
                }
            }
            return res;
        }

        public void Create(Track t)
        {
            _database.Insert(t);
        }

        public List<Track> GetBy(Expression<Func<Track, bool>> pred)
        {
            return _tracks.Where(pred).ToList();
        }

        public Track GetItem(int id)
        {
            var res = _tracks.Where(t => t.Id == id).FirstOrDefault();
            try
            {
                res.CD = App.Cds.GetItem(res.CdId);
            } catch (Exception ex)
            {
                res.CD = new CD()
                {
                    Name = "No CD",
                    Id = -1,
                    Index = -1
                };
            }
            return res;
        }

        public void SaveItem(Track item)
        {
            Track t = GetItem(item.Id);
            t.Index = item.Index;
            t.Note1 = item.Note1;
            t.Note2 = item.Note2;
            t.Note3 = item.Note3;
            t.CdId = item.CdId;
            t.Title = item.Title;
            _database.Update(t);
        }

        public int DeleteItem(Track item)
        {
            return  _tracks.Delete(i => i.Id == item.Id);
        }

    }
}
