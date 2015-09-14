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
using MyOwnWords.WP.Extensions;
using Windows.Storage;

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

        private bool DropDatabase { get; set; }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="_dropDatabase">If you want drop and create database then true. Default is false.</param>
        public Seed(bool _dropDatabase = false)
        {
            this.DropDatabase = _dropDatabase;
        }

        /// <summary>
        /// Generating sample data
        /// </summary>
        public void SampleData()
        {
            CreateDatabaseIfNotExists();

            using (var dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
            {
                var user = GetUser("sutak.jakub@gmail.com", "Jey");
                int userId = -1;

                userId = dbConn.Insert(user);

                for (int i = 0; i < 10; i++)
                {
                    dbConn.Insert(GetPhoto(3));
                }


                //var myOwnWord2 = GetMyOwnWord(userId, "Man");
                //int myOwnWord2Id = -1;

                //dbConn.RunInTransaction(() =>
                //{
                //    dbConn.InsertWithChildren(myOwnWord2);
                //});

                //var Photo = GetPhoto(myOwnWordId);
                //var PhotoId = -1;

                //dbConn.RunInTransaction(() =>
                //{
                //    dbConn.InsertWithChildren(Photo);
                //});

                //var Photo2 = GetPhoto(myOwnWordId);
                //var Photo2Id = -1;

                //dbConn.RunInTransaction(() =>
                //{
                //    dbConn.InsertWithChildren(Photo2);
                //});

                //var Photo3 = GetPhoto(myOwnWord2Id);
                //var Photo3Id = -1;
                //dbConn.RunInTransaction(() =>
                //{
                //    dbConn.InsertWithChildren(Photo3);
                //});

                //test for getting
                //var e = dbConn.GetWithChildren<MyOwnWord>(myOwnWord.MyOwnWordID);

                var u = dbConn.GetWithChildren<User>(3);
                var e = dbConn.GetWithChildren<MyOwnWord>(3);

            }
        }

        private async void DeleteDatabase()
        {
            StorageFile file = null;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(App.DbPath);
                await file.DeleteAsync();
            }
            catch (Exception ex)
            {
                file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(App.DbPath);
            }
            
        }

        private void CreateDatabaseIfNotExists()
        {
            try
            {
                if (DropDatabase)
                {
                    DeleteDatabase();
                }

                var store = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(App.DbPath);

                if (store != null)
                {
                    using (dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
                    {
                        dbConn.CreateTable<User>();
                        dbConn.CreateTable<Sentence>();
                        dbConn.CreateTable<Recording>();
                        dbConn.CreateTable<Photo>();
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
        /// Returns filled sample Photo.
        /// </summary>
        /// <param name="myOwnWordId">ID of MyOwnWord</param>
        /// <returns></returns>
        private Photo GetPhoto(int myOwnWordId)
        {
            Photo result = new Photo();

            result.PhotoUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.Data = new byte[] { 0, 1 };
            result.MyOwnWordId = myOwnWordId;

            return result;
        }
    }
}
