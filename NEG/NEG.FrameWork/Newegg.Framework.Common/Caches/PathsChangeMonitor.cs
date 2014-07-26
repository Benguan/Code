using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Caching;
using Newegg.Framework.Implement;
using Newegg.Framework.IO;
using Newegg.Framework.IO.Implement;

namespace Newegg.Framework.Caches
{
    /// <summary>
    /// Paths change monitor.
    /// </summary>
    internal class PathsChangeMonitor : FileChangeMonitor
    {
        /// <summary>
        /// The now.
        /// </summary>
        private static readonly INow now = new StaticNow();

        /// <summary>
        /// The static path.
        /// </summary>
        private static readonly IStaticPath staticPath = new StaticPath();

        /// <summary>
        /// The static directory.
        /// </summary>
        private static readonly IStaticDirectory staticDirectory = new StaticDirectory();

        /// <summary>
        /// Static file.
        /// </summary>
        private static readonly IStaticFile staticFile = new StaticFile();

        /// <summary>
        /// The utility.
        /// </summary>
        private static readonly IPathUtility utility = new DefaultPathUtility(staticPath, staticDirectory, staticFile);

        /// <summary>
        /// The watcher factory.
        /// </summary>
        private static readonly IDirectoryWatcherFactory watcherFactory = new DirectoryWatcherFactory(utility);

        /// <summary>
        /// My guid.
        /// </summary>
        private readonly string myGuid;

        /// <summary>
        /// Directory watchers.
        /// </summary>
        private readonly IEnumerable<IDirectoryWatcher> watchers;

        /// <summary>
        /// Watched paths.
        /// </summary>
        private readonly ReadOnlyCollection<string> paths;

        /// <summary>
        /// My last modify time.
        /// </summary>
        private DateTimeOffset myLastModifyTime;

        /// <summary>
        /// Initializes a new instance of the PathsChangeMonitor class.
        /// </summary>
        /// <param name="paths">Monitored paths.</param>
        /// <exception cref="System.ArgumentException">Paths are null or empty.;paths</exception>
        public PathsChangeMonitor(IEnumerable<string> paths)
        {
            if (paths.IsNullOrEmpty())
            {
                throw new ArgumentException("Paths are null or empty.", "paths");
            }

            this.watchers = paths.Select(path =>
            {
                IDirectoryWatcher watcher = watcherFactory.CreateWatcher(path);

                if (!string.IsNullOrWhiteSpace(watcher.Path))
                {
                    if (staticPath.HasExtension(path))
                    {
                        watcher.AddCreatedHandler((sender, args) =>
                        {
                            if (string.Equals(path, args.FullPath, StringComparison.OrdinalIgnoreCase))
                            {
                                this.OnChanged(null);
                                this.myLastModifyTime = now.DateTimeOffset;
                            }
                        });

                        watcher.AddChangedHandler((sender, args) =>
                        {
                            if (string.Equals(path, args.FullPath, StringComparison.OrdinalIgnoreCase))
                            {
                                this.OnChanged(null);
                                this.myLastModifyTime = now.DateTimeOffset;
                            }
                        });

                        watcher.AddDeletedHandler((sender, args) =>
                        {
                            if (string.Equals(path, args.FullPath, StringComparison.OrdinalIgnoreCase))
                            {
                                this.OnChanged(null);
                                this.myLastModifyTime = now.DateTimeOffset;
                            }
                        });

                        watcher.AddRenamedHandler((sender, args) =>
                        {
                            if (string.Equals(path, args.OldFullPath, StringComparison.OrdinalIgnoreCase))
                            {
                                this.OnChanged(null);
                                this.myLastModifyTime = now.DateTimeOffset;
                            }
                            else if (staticDirectory.Exists(args.OldFullPath))
                            {
                                if (path.ToLower().StartsWith(args.OldFullPath.ToLower()))
                                {
                                    this.OnChanged(null);
                                    this.myLastModifyTime = now.DateTimeOffset;
                                }
                            }
                        });
                    }
                    else
                    {
                        watcher.AddCreatedHandler((sender, args) =>
                        {
                            this.OnChanged(null);
                            this.myLastModifyTime = now.DateTimeOffset;
                        });

                        watcher.AddChangedHandler((sender, args) =>
                        {
                            this.OnChanged(null);
                            this.myLastModifyTime = now.DateTimeOffset;
                        });

                        watcher.AddDeletedHandler((sender, args) =>
                        {
                            this.OnChanged(null);
                            this.myLastModifyTime = now.DateTimeOffset;
                        });

                        watcher.AddRenamedHandler((sender, args) =>
                        {
                            this.OnChanged(null);
                            this.myLastModifyTime = now.DateTimeOffset;
                        });
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format("{0} is an invalid path.", path), "paths");
                }

                return watcher;
            }).ToArray();

            this.paths = paths.ToList().AsReadOnly();

            this.myGuid = Guid.NewGuid().ToString();

            this.InitializationComplete();
        }

        /// <summary>
        /// Gets a collection that contains the paths of files that are monitored for changes.
        /// </summary>
        public override ReadOnlyCollection<string> FilePaths
        {
            get { return this.paths; }
        }

        /// <summary>
        /// Gets a value that indicates the last time that a file that is being monitored was changed.
        /// </summary>
        public override DateTimeOffset LastModified
        {
            get { return this.myLastModifyTime; }
        }

        /// <summary>
        /// Gets a value that represents the System.Runtime.Caching.ChangeMonitor class instance.
        /// </summary>
        public override string UniqueId
        {
            get { return this.myGuid; }
        }

        /// <summary>
        /// Releases all managed and unmanaged resources and any references to the System.Runtime.Caching.ChangeMonitor instance. This overload must be implemented by derived change-monitor classes.
        /// </summary>
        /// <param name="disposing">True to release managed and unmanaged resources and any references to a System.Runtime.Caching.ChangeMonitor instance; false to release only unmanaged resources. When false is passed, the System.Runtime.Caching.ChangeMonitor.Dispose(System.Boolean) method is called by a finalizer thread and any external managed references are likely no longer valid because they have already been garbage collected.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.watchers.IsNullOrEmpty())
                {
                    this.watchers.ForEach(watcher => watcher.Dispose());
                }
            }
        }
    }
}
