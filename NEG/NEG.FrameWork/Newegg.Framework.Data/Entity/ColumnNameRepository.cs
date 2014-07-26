using System.Collections.Concurrent;

namespace Newegg.Framework.Entity
{
    /// <summary>
    /// Class UpperRepository.
    /// </summary>
    public static class UpperRepository
    {
        /// <summary>
        /// The uppers.
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> Uppers = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Gets the upper.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>System.String.</returns>
        public static string GetUpper(string original)
        {
            if (string.IsNullOrWhiteSpace(original))
            {
                return string.Empty;
            }

            return Uppers.GetOrAdd(original, GenerateUpper);
        }

        /// <summary>
        /// Generates the upper.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>System.String.</returns>
        public static string GenerateUpper(string original)
        {
            return original.ToUpper();
        }
    }
}
