using System;
using System.Collections.Generic;
using System.IO;

namespace Newegg.Framework.IO.Implement
{
    /// <summary>
    /// File system watcher directory watcher.
    /// </summary>
    internal class FileSystemWatcherDirectoryWatcher : IDirectoryWatcher
    {
        /// <summary>
        /// My watcher.
        /// </summary>
        private FileSystemWatcher watcher = null;

        /// <summary>
        /// Created handler.
        /// </summary>
        private List<Action<object, FileSystemEventArgs>> createdHandlers = null;

        /// <summary>
        /// Changed handler.
        /// </summary>
        private List<Action<object, FileSystemEventArgs>> changedHandlers = null;

        /// <summary>
        /// Deleted handler.
        /// </summary>
        private List<Action<object, FileSystemEventArgs>> deleltedHandlers = null;

        /// <summary>
        /// Renamed handler.
        /// </summary>
        private List<Action<object, RenamedEventArgs>> renamedHandlers = null;

        /// <summary>
        /// Initializes a new instance of the FileSystemWatcherDirectoryWatcher class.
        /// </summary>
        /// <param name="path">Full requested path.</param>
        /// <param name="utility">Path utility.</param>
        internal FileSystemWatcherDirectoryWatcher(string path, IPathUtility utility)
        {
            this.Path = utility.GetExistDirectoryOrParentDirectory(path);

            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                this.watcher = new FileSystemWatcher(this.Path);
                this.watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
                this.watcher.IncludeSubdirectories = true;
                this.watcher.Created += this.OnCreated;
                this.watcher.Changed += this.OnChanged;
                this.watcher.Deleted += this.OnDeleted;
                this.watcher.Renamed += this.OnRenamed;
                this.watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Gets full directory path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.watcher != null)
            {
                this.watcher.Dispose();
            }
        }

        /// <summary>
        /// Add created handler.
        /// </summary>
        /// <param name="createdHandler">Created handler.</param>
        public void AddCreatedHandler(Action<object, FileSystemEventArgs> createdHandler)
        {
            if (createdHandler != null && this.watcher != null)
            {
                if (this.createdHandlers == null)
                {
                    this.createdHandlers = new List<Action<object, FileSystemEventArgs>>();
                }

                this.createdHandlers.Add(createdHandler);
            }
        }

        /// <summary>
        /// Add changed handler.
        /// </summary>
        /// <param name="changedHandler">Changed handler.</param>
        public void AddChangedHandler(Action<object, FileSystemEventArgs> changedHandler)
        {
            if (changedHandler != null && this.watcher != null)
            {
                if (this.changedHandlers == null)
                {
                    this.changedHandlers = new List<Action<object, FileSystemEventArgs>>();
                }

                this.changedHandlers.Add(changedHandler);
            }
        }

        /// <summary>
        /// Add deleted handler.
        /// </summary>
        /// <param name="deletedHandler">Deleted handler.</param>
        public void AddDeletedHandler(Action<object, FileSystemEventArgs> deletedHandler)
        {
            if (deletedHandler != null && this.watcher != null)
            {
                if (this.deleltedHandlers == null)
                {
                    this.deleltedHandlers = new List<Action<object, FileSystemEventArgs>>();
                }

                this.deleltedHandlers.Add(deletedHandler);
            }
        }

        /// <summary>
        /// Add renamed handler.
        /// </summary>
        /// <param name="renamedHandler">Renamed handler.</param>
        public void AddRenamedHandler(Action<object, RenamedEventArgs> renamedHandler)
        {
            if (renamedHandler != null && this.watcher != null)
            {
                if (this.renamedHandlers == null)
                {
                    this.renamedHandlers = new List<Action<object, RenamedEventArgs>>();
                }

                this.renamedHandlers.Add(renamedHandler);
            }
        }

        /// <summary>
        /// On path changed event.
        /// </summary>
        /// <param name="source">Changed source.</param>
        /// <param name="e">Path change event args.</param>
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            if (!this.createdHandlers.IsNullOrEmpty())
            {
                this.createdHandlers.ForEach(action => action(source, e));
            }
        }

        /// <summary>
        /// On path changed event.
        /// </summary>
        /// <param name="source">Changed source.</param>
        /// <param name="e">Path change event args.</param>
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            if (!this.deleltedHandlers.IsNullOrEmpty())
            {
                this.deleltedHandlers.ForEach(action => action(source, e));
            }
        }

        /// <summary>
        /// On path changed event.
        /// </summary>
        /// <param name="source">Changed source.</param>
        /// <param name="e">Path change event args.</param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!this.changedHandlers.IsNullOrEmpty())
            {
                this.changedHandlers.ForEach(action => action(source, e));
            }
        }

        /// <summary>
        /// On path rename event.
        /// </summary>
        /// <param name="source">Rename source.</param>
        /// <param name="e">Path rename args.</param>
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (!this.renamedHandlers.IsNullOrEmpty())
            {
                this.renamedHandlers.ForEach(action => action(source, e));
            }
        }
    }
}
