using System;

namespace Intersect.Config
{
    /// <summary>
    /// Contains all options pertaining to factions
    /// </summary>
    public class FactionOptions
    {

        /// <summary>
        /// Denotes the minimum amount of characters a 
        /// name must contain before being accepted.
        /// </summary>
        public int MinimumFactionNameSize = 3;

        /// <summary>
        /// Denotes the maximum amount of characters a faction name can contain before being rejected.
        /// </summary>
        public int MaximumFactionNameSize = 10;
    }
}