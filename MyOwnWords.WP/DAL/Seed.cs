using MyOwnWords.WP.DAL.SQLite;
using MyOwnWords.WP.DataModel;
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
            CreateDatabaseIfNotExists(App.DbPath);
            var user = GetUser("sutak.jakub@gmail.com", "Jey");

            using (var dbConn = new SQLiteConnection(App.DbPath))
            { 
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(user);
                });
            }

            //var myOwnWord = GetMyOwnWord();
        }

        private void CreateDatabaseIfNotExists(string dbPath)
        {
            try
            {
                var store = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(App.DbPath);
                if (store != null)
                {
                    using (dbConn = new SQLiteConnection(dbPath))
                    {
                        dbConn.CreateTable<User>();
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

        //private MyOwnWord GetMyOwnWord()
        //{
        //    MyOwnWord result = new MyOwnWord();

        //    result.Created = DateTime.UtcNow;
        //    result.Updated = DateTime.UtcNow;
        //    result.User = UserId
        //}
    }
}
