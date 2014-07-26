using System;

namespace Newegg.Framework.Implement
{
    /// <summary>
    /// Static now.
    /// </summary>
    internal class StaticNow : INow
    {
        /// <summary>
        /// Gets date time now.
        /// </summary>
        public DateTime DateTime
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Gets date time off set now.
        /// </summary>
        public DateTimeOffset DateTimeOffset
        {
            get { return DateTimeOffset.Now; }
        }
    }
}
