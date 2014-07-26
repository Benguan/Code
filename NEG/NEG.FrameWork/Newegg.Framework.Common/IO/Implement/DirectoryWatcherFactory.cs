namespace Newegg.Framework.IO.Implement
{
    /// <summary>
    /// Dirctory watch factory.
    /// </summary>
    internal class DirectoryWatcherFactory : IDirectoryWatcherFactory
    {
        /// <summary>
        /// Path utility.
        /// </summary>
        private IPathUtility utility = null;

        /// <summary>
        /// Initializes a new instance of the DirectoryWatcherFactory class.
        /// </summary>
        /// <param name="utility">Path utility.</param>
        public DirectoryWatcherFactory(IPathUtility utility)
        {
            this.utility = utility;
        }

        /// <summary>
        /// Create directory watcher.
        /// </summary>
        /// <param name="path">Full path.</param>
        /// <returns>Directory watcher.</returns>
        public IDirectoryWatcher CreateWatcher(string path)
        {
            return new FileSystemWatcherDirectoryWatcher(path, this.utility);
        }
    }
}
