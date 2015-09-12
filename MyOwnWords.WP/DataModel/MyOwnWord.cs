using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace MyOwnWords.WP.DataModel
{
    /// <summary>
    /// Class represents MyOwnWord in application.
    /// </summary>
    [Table("MyOwnWord")]
    public class MyOwnWord
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int MyOwnWordID { get; set; }

        /// <summary>
        /// Unique identificator with not null constraint.
        /// GUID format.
        /// </summary>
        [MaxLength(36), Unique, NotNull]
        public string MyOwnWordUID { get; set; }

        /// <summary>
        /// Word name with maximum length of 100 characters.
        /// Cannot be null.
        /// </summary>
        [MaxLength(100), NotNull]
        public string WordName { get; set; }

        /// <summary>
        /// Created date/time.
        /// Cannot be null.
        /// Every datetime is in UTC format.
        /// </summary>
        [NotNull]
        public DateTime Created { get; set; }

        /// <summary>
        /// Updated date/time.
        /// Cannot be null.
        /// Every datetime is in UTC format.
        /// </summary>
        [NotNull]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Foreign key for User.
        /// </summary>
        [ForeignKey(typeof(User))]
        public int UserID { get; set; }

        /// <summary>
        /// Every word has got owner.
        /// </summary>
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Every word has got 0..n pictures.
        /// </summary>
        //[OneToMany]
        //public List<Picture> Pictures { get; set; }
    }
}
