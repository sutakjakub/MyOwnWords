using MyOwnWords.WP.DataModel;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnWords.WP.DAL
{
    /// <summary>
    /// This class creating database and has got sample data for seed database.
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Database connection for this instance.
        /// </summary>
        private SQLiteConnection dbConn;

        /// <summary>
        /// Generating sample data
        /// </summary>
        public void SampleData()
        {
            CreateDatabaseIfNotExists(true);

            using (var dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
            {
                var user = GetUser("sutak.jakub@gmail.com", "Jey");
                int userId = -1;

                dbConn.RunInTransaction(() =>
                {
                    userId = dbConn.Insert(user);
                });

                var myOwnWord = GetMyOwnWord(userId, "Hello");
                int myOwnWordId = -1;

                dbConn.RunInTransaction(() =>
                {
                    myOwnWordId = dbConn.Insert(myOwnWord);
                });
            }
        }

        private void CreateDatabaseIfNotExists(bool drop = false)
        {
            try
            {
                var store = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(App.DbPath);
                if (store != null && drop)
                {
                    using (dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
                    {
                        dbConn.CreateTable<User>();
                        dbConn.CreateTable<MyOwnWord>();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns filled sample user.
        /// </summary>
        /// <param name="email">Email of user.</param>
        /// <param name="nickname">Nickname of user.</param>
        /// <returns>Returns MyOwnWords.WP.DataModel.User</returns>
        private User GetUser(string email, string nickname)
        {
            User result = new User();

            result.UserUID = Guid.NewGuid().ToString();
            result.Email = email;
            result.Nickname = nickname;
            result.Created = DateTime.UtcNow;
            result.Updated = DateTime.UtcNow;

            return result;
        }

        private MyOwnWord GetMyOwnWord(int userId, string word)
        {
            MyOwnWord result = new MyOwnWord();

            result.MyOwnWordUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.Updated = DateTime.UtcNow;
            result.WordName = word;
            result.UserId = userId;

            return result;
        }
    }
}
