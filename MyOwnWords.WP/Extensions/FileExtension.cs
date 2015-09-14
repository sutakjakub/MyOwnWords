using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyOwnWords.WP.Extensions
{
    public static class FileExtension
    {
        /// <summary>
        /// Returns true if file exists then false.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<bool> IsFileExists(this StorageFolder folder, string fileName)
        {
            return (await folder.GetFilesAsync()).Any(x => x.Name.Equals(fileName));
        }
    }
}
