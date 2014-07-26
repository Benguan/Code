namespace Newegg.Framework.IO
{
    /// <summary>
    /// Path utility interface.
    /// </summary>
    internal interface IPathUtility
    {
        /// <summary>
        /// Get path directory.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>If path is a file, return file directory full path. If path is a directory, return the directory. If path is not exist, return string.Empty.</returns>
        string GetDirectory(string path);

        /// <summary>
        /// Get the first exist directory or parent directory.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>If path is a file, return the first exist directory or parent directory full path. If path is a directory, return it self or first exist parent directory. If all parent directies are not exist, return string.Empty.</returns>
        string GetExistDirectoryOrParentDirectory(string path);
    }
}
