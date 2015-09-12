using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnWords.WP.DataModel
{
    /// <summary>
    /// This class represents Photo for MyOwnWord.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int PhotoID { get; set; }

        /// <summary>
        /// Unique identificator.
        /// Cannot be null.
        /// GUID used.
        /// </summary>
        [MaxLength(36), Unique, NotNull]
        public string PhotoUID { get; set; }

        /// <summary>
        /// Created date/time.
        /// Cannot be null.
        /// Every datetime is in UTC format.
        /// </summary>
        [NotNull]
        public DateTime Created { get; set; }
        
        /// <summary>
        /// Name of recording. Can be null.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Bytes of Photo.
        /// Canot be null.
        /// </summary>
        [NotNull]
        public byte[] Data { get; set; }

        /// <summary>
        /// Foreign key for MyOwnWord.
        /// </summary>
        [ForeignKey(typeof(MyOwnWord))]
        public int MyOwnWordId { get; set; }

        /// <summary>
        /// Photo belong only one MyOwnWord
        /// </summary>
        [ManyToOne]
        public MyOwnWord MyOwnWord { get; set; }
    }
}
