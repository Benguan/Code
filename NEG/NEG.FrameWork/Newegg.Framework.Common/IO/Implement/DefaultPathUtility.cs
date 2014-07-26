namespace Newegg.Framework.IO.Implement
{
    /// <summary>
    /// Defualt path utility.
    /// </summary>
    internal class DefaultPathUtility : IPathUtility
    {
        /// <summary>
        /// Static path.
        /// </summary>
        private IStaticPath staticPath = null;

        /// <summary>
        /// Static directory.
        /// </summary>
        private IStaticDirectory staticDirectory = null;

        /// <summary>
        /// Static file.
        /// </summary>
        private IStaticFile staticFile = null;

        /// <summary>
        /// Initializes a new instance of the DefaultPathUtility class.
        /// </summary>
        /// <param name="staticPath">Static path.</param>
        /// <param name="staticDirectory">Static directory.</param>
        /// <param name="staticFile">Static file.</param>
        public DefaultPathUtility(IStaticPath staticPath, IStaticDirectory staticDirectory, IStaticFile staticFile)
        {
            this.staticPath = staticPath;
            this.staticDirectory = staticDirectory;
            this.staticFile = staticFile;
        }

        /// <summary>
        /// Get path directory.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>If path is a file, return file directory full path. If path is a directory, return the directory. If path is not exist, return string.Empty.</returns>
        public string GetDirectory(string path)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(path) && this.staticPath.IsPathRooted(path))
            {
                if (this.staticFile.Exists(path))
                {
                    result = this.staticPath.GetDirectoryName(path);
                }
                else if (this.staticDirectory.Exists(path))
                {
                    result = path;
                }
            }

            return result;
        }

        /// <summary>
        /// Get the first exist directory or parent directory.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>If path is a file, return the first exist directory or parent directory full path. If path is a directory, return it self or first exist parent directory. If all parent directies are not exist, return string.Empty.</returns>
        public string GetExistDirectoryOrParentDirectory(string path)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(path) && this.staticPath.IsPathRooted(path))
            {
                result = this.GetDirectory(path);

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = this.GetExistDirectoryOrParentDirectory(this.staticPath.GetDirectoryName(path));
                }
            }

            return result;
        }
    }
}
