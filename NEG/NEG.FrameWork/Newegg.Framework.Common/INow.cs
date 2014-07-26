using System;

namespace Newegg.Framework
{
    /// <summary>
    /// Now interface.
    /// </summary>
    internal interface INow
    {
        /// <summary>
        /// Gets date time now.
        /// </summary>
        DateTime DateTime { get; }

        /// <summary>
        /// Gets date time off set now.
        /// </summary>
        DateTimeOffset DateTimeOffset { get; }
    }
}
