namespace Newegg.Framework.IO
{
    /// <summary>
    /// Directory watcher factory.
    /// </summary>
    internal interface IDirectoryWatcherFactory
    {
        /// <summary>
        /// Create directory watcher.
        /// </summary>
        /// <param name="path">Full path.</param>
        /// <returns>Directory watcher.</returns>
        IDirectoryWatcher CreateWatcher(string path);
    }
}
