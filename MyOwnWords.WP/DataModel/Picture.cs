using MyOwnWords.WP.DAL.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnWords.WP.DataModel
{
    /// <summary>
    /// This class represents Picture for MyOwnWord.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int PictureID { get; set; }

        /// <summary>
        /// Unique identificator.
        /// Cannot be null.
        /// GUID used.
        /// </summary>
        [MaxLength(36), Unique, NotNull]
        public string PictureUID { get; set; }

        /// <summary>
        /// Created date/time.
        /// Cannot be null.
        /// Every datetime is in UTC format.
        /// </summary>
        [NotNull]
        public DateTime Created { get; set; }

        /// <summary>
        /// Bytes of picture.
        /// </summary>
        [NotNull]
        public byte[] Data { get; set; }
    }
}
