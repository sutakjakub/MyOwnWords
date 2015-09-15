using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnWords.WP.DataModel
{
    /// <summary>
    /// Represents record from vw_mowlist. This is one item in main List (with MyOwnWord).
    /// </summary>
    public class MowListItem
    {
        /// <summary>
        /// Id of MyOwnWord
        /// </summary>
        public int MyOwnWordID { get; set; }
        /// <summary>
        /// UID of MyOwnWord 
        /// </summary>
        public string MyOwnWordUID { get; set; }
        /// <summary>
        /// Word name
        /// </summary>
        public string WordName { get; set; }
        /// <summary>
        /// Translate of Word Name.
        /// </summary>
        public string Translate { get; set; }
        /// <summary>
        /// Is exists photo?
        /// </summary>
        public bool IsExistsPhoto { get; set; }
        /// <summary>
        /// Is exists sentence?
        /// </summary>
        public bool IsExistsSentence { get; set; }
        /// <summary>
        /// Is exists recording?
        /// </summary>
        public bool IsExistsRecording { get; set; }
        /// <summary>
        /// ID of user, owner of MyOwnWord.
        /// </summary>
        public int UserID { get; set; }
    }
}
