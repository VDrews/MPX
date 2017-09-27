using System.IO;
using Windows.Storage;
using MPX;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(WindowsSQLitePlatform))]
namespace MPX
{
    public class WindowsSQLitePlatform : IDatabaseConnection
    {
        private string GetPath()
        {
            var dbName = "somostechies.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return path;
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }
        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(GetPath());
        }
    }
}
