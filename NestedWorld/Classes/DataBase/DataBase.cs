using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.DataBase
{
    public class DataBase
    {
        protected string path;
        protected SQLite.Net.SQLiteConnection connection {
            get
            {
                return new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            }
            set { }
        }



        public DataBase(string path)
        {
            this.path = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, path);
        }

    }
}
