using MyOwnWords.WP.DataModel;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SQLite.Net.Async;
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
                    dbConn.InsertWithChildren(myOwnWord, true);
                });

                var myOwnWord2 = GetMyOwnWord(userId, "Man");
                int myOwnWord2Id = -1;

                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(myOwnWord2);
                });

                var picture = GetPicture(myOwnWordId);
                var pictureId = -1;

                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(picture);
                });

                var picture2 = GetPicture(myOwnWordId);
                var picture2Id = -1;

                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(picture2);
                });

                var picture3 = GetPicture(myOwnWord2Id);
                var picture3Id = -1;
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(picture3);
                });

                //test for getting
                var entities = dbConn.GetAllWithChildren<MyOwnWord>();
                var maping = dbConn.GetMapping<MyOwnWord>();

                
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
                        dbConn.CreateTable<Picture>();
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
            result.UserID = userId;

            return result;
        }

        /// <summary>
        /// Returns filled sample Picture.
        /// </summary>
        /// <param name="myOwnWordId">ID of MyOwnWord</param>
        /// <returns></returns>
        private Picture GetPicture(int myOwnWordId)
        {
            Picture result = new Picture();

            result.PictureUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.Data = new byte[] { 0, 1 };
            result.MyOwnWordId = myOwnWordId;

            return result;
        }
    }
}
