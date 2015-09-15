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
using System.IO;

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
        /// Generating sample data.
        /// </summary>
        public async Task<bool> SampleData()
        {
            await DropAndCreateDatabase();

            using (var dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
            {
                //create user
                var user = GetUser("sutak.jakub@gmail.com", "Jey");
                dbConn.Insert(user);

                //create myOwnWord
                var mow = GetMyOwnWord(user.UserID, "Man", "Muž, Chlap");
                dbConn.Insert(mow);

                //create second myOwnWord
                var mow2 = GetMyOwnWord(user.UserID, "Hello", "Ahoj");
                dbConn.Insert(mow2);
                
                //create 10 photos belongs to mow
                for (int i = 0; i < 10; i++)
                {
                    dbConn.Insert(GetPhoto(mow.MyOwnWordID));
                }

                //create 5 recordings belongs to mow
                for (int i = 0; i < 5; i++)
                {
                    dbConn.Insert(GetRecording(mow.MyOwnWordID));
                }

                //create sentence belongs to mow
                dbConn.Insert(GetSentence(mow.MyOwnWordID, "This man is amazing!"));
                dbConn.Insert(GetSentence(mow2.MyOwnWordID, "Hello my love :-*"));

                MowListItem item = new MowListItem();
                var listItems = dbConn.Query<MowListItem>("select * from vw_mowlist");
            }

            return true;
        }

        /// <summary>
        /// Delete SQLite file (database).
        /// </summary>
        /// <returns></returns>
        private async Task<bool> DeleteDatabase()
        {
            try
            {
                StorageFolder local = ApplicationData.Current.LocalFolder;
                StorageFile file = await local.GetFileAsync(App.DbName);

                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

                return true;
            }
            catch (Exception e)
            {
                //indicates that file doesn't exists
            }

            return false;
        }

        /// <summary>
        /// Drop and create database.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> DropAndCreateDatabase()
        {
            await DeleteDatabase();

            using (dbConn = new SQLiteConnection(new SQLitePlatformWinRT(), App.DbPath))
            {
                dbConn.Execute("PRAGMA encoding='UTF-8'");

                dbConn.CreateTable<User>();
                dbConn.CreateTable<Sentence>();
                dbConn.CreateTable<Recording>();
                dbConn.CreateTable<Photo>();
                dbConn.CreateTable<MyOwnWord>();

                //create vw_mowlist
                string vw_mowlist = await Get_vw_mowlist();
                int i = dbConn.Execute(vw_mowlist);

            }

            return true;
        }

        private async Task<string> Get_vw_mowlist()
        {
            try
            {
                string fileContent;
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///SQL files/vw_mowlist.sql"));
                using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
                {
                    fileContent = await sRead.ReadToEndAsync();
                }

                return fileContent;
            }
            catch (Exception ex)
            {
                return string.Empty;
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

        /// <summary>
        /// Returns sample MyOwnWord
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="word"></param>
        /// <param name="translate"></param>
        /// <returns></returns>
        private MyOwnWord GetMyOwnWord(int userId, string word, string translate)
        {
            MyOwnWord result = new MyOwnWord();

            result.MyOwnWordUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.Updated = DateTime.UtcNow;
            result.WordName = word;
            result.UserID = userId;
            result.Translate = translate;

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

        /// <summary>
        /// Returns sample Recording.
        /// </summary>
        /// <param name="myOwnWordId"></param>
        /// <returns></returns>
        private Recording GetRecording(int myOwnWordId)
        {
            Recording result = new Recording();

            result.RecordingUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.Data = new byte[] { 0, 1 };
            result.MyOwnWordId = myOwnWordId;
            result.Length = double.Parse(new Random().Next(0, 5).ToString());
            result.Name = "Recording name: " + result.Length + myOwnWordId;

            return result;
        }

        /// <summary>
        /// Returns sample sentence.
        /// </summary>
        /// <param name="myOwnWordId"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private Sentence GetSentence(int myOwnWordId, string sentence)
        {
            Sentence result = new Sentence();

            result.SentenceUID = Guid.NewGuid().ToString();
            result.Created = DateTime.UtcNow;
            result.MyOwnWordId = myOwnWordId;
            result.Translate = sentence;

            return result;
        }
    }
}
