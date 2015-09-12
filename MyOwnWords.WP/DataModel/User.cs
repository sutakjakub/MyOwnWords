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
    /// This class represents User in application.
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        /// <summary>
        /// Unique identificator.
        /// Cannot be null.
        /// GUID used.
        /// </summary>
        [MaxLength(36), Unique, NotNull]
        public string UserUID { get; set; }

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
        /// User's email.
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// User's nickname.
        /// </summary>
        [MaxLength(100)]
        public string Nickname { get; set; }

        /// <summary>
        /// Users' every added word and his dependency (as record, picture and so on).
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MyOwnWord> MyOwnWords { get; set; }
    }
}
