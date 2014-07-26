using System;
using System.IO;

namespace Newegg.Framework.IO
{
    /// <summary>
    /// Directory watcher.
    /// </summary>
    internal interface IDirectoryWatcher : IDisposable
    {
        /// <summary>
        /// Gets full directory path.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Add created handler.
        /// </summary>
        /// <param name="createdHandler">Created handler.</param>
        void AddCreatedHandler(Action<object, FileSystemEventArgs> createdHandler);

        /// <summary>
        /// Add changed handler.
        /// </summary>
        /// <param name="changedHandler">Changed handler.</param>
        void AddChangedHandler(Action<object, FileSystemEventArgs> changedHandler);

        /// <summary>
        /// Add deleted handler.
        /// </summary>
        /// <param name="deletedHandler">Deleted handler.</param>
        void AddDeletedHandler(Action<object, FileSystemEventArgs> deletedHandler);

        /// <summary>
        /// Add renamed handler.
        /// </summary>
        /// <param name="renamedHandler">Renamed handler.</param>
        void AddRenamedHandler(Action<object, RenamedEventArgs> renamedHandler);
    }
}
