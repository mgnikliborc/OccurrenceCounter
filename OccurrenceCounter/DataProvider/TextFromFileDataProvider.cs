using System.IO;
using OccurrenceCounter.SignsRemover;

namespace OccurrenceCounter.DataProvider
{
    /// <summary>
    /// Class which provides data from text file.
    /// </summary>
    public class TextFromFileDataProvider : TextDataProvider
    {
        #region Constructors

        /// <summary>
        /// Provider constructor.
        /// </summary>
        public TextFromFileDataProvider() : base()
        {
        }

        /// <summary>
        /// Provider constructor.
        /// </summary>
        /// <param name="remover">Instance of <see cref="ISignsRemover<string>"/>, which will remove not needed signs.</param>
        public TextFromFileDataProvider(ISignsRemover<string> remover)
            : base(remover)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Reads data from text file on specified path and sets provider's data.
        /// </summary>
        /// <param name="path">Path to the text file.</param>
        /// <exception cref="System.ArgumentException">Path is a zero-length string, contains only white space, or contains one or more
        ///     invalid characters as defined by System.IO.Path.InvalidPathChars.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.
        ///     For example, on Windows-based platforms, paths must be less than 248 characters,
        ///     and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.UnauthorizedAccessException">Path specified a file that is read-only.-or- This operation is not supported
        ///     on the current platform.-or- path specified a directory.-or- The caller does
        ///     not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
        /// <exception cref="System.NotSupportedException">Path is in an invalid format.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        public override void SetData(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                this.Text = this.ReadFromFile(path);
            }
        }

        /// <summary>
        /// Reads data from text file on specified path.
        /// </summary>
        /// <param name="path">Path to the text file.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Path is a zero-length string, contains only white space, or contains one or more
        ///     invalid characters as defined by System.IO.Path.InvalidPathChars.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.
        ///     For example, on Windows-based platforms, paths must be less than 248 characters,
        ///     and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.UnauthorizedAccessException">Path specified a file that is read-only.-or- This operation is not supported
        ///     on the current platform.-or- path specified a directory.-or- The caller does
        ///     not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
        /// <exception cref="System.NotSupportedException">Path is in an invalid format.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        protected virtual string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }
        #endregion
    }
}
