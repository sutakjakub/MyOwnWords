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
    /// This class represents Sentence for MyOwnWord.
    /// </summary>
    public class Sentence
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int SentenceID { get; set; }

        /// <summary>
        /// Unique identificator.
        /// Cannot be null.
        /// GUID used.
        /// </summary>
        [MaxLength(36), Unique, NotNull]
        public string SentenceUID { get; set; }

        /// <summary>
        /// Created date/time.
        /// Cannot be null.
        /// Every datetime is in UTC format.
        /// </summary>
        [NotNull]
        public DateTime Created { get; set; }

        /// <summary>
        /// String of sentences for determined MyOwnWord.
        /// </summary>
        public string Translate { get; set; }

        /// <summary>
        /// Foreign key for MyOwnWord.
        /// </summary>
        [ForeignKey(typeof(MyOwnWord))]
        public int MyOwnWordId { get; set; }

        /// <summary>
        /// Sentence belong only one MyOwnWord
        /// </summary>
        [ManyToOne]
        public MyOwnWord MyOwnWord { get; set; }
    }
}
