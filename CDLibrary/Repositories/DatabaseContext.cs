using CDLibrary.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Repositories
{
    public class DatabaseContext
    {
        public TableQuery<Track> Tracks { get; set; }
        public TableQuery<CD> Cds { get; set; }

        public SQLiteConnection Database { get; private set; }
        private readonly string _databaseName = "CdLibrary.db3";
        private readonly SQLiteOpenFlags _flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public DatabaseContext()
        {
            string connectionString = Path.Combine(FileSystem.AppDataDirectory, _databaseName);
            Database = new SQLiteConnection(connectionString, _flags);
            Init();
        }

        public void Init()
        {
            var tracks = Database.CreateTable<Track>();
            var cds = Database.CreateTable<CD>();

            Tracks = Database.Table<Track>();
            Cds = Database.Table<CD>();
        }
    }
}
