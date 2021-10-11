using System;
namespace Yatter.Invigoration.Azure.Models
{
    /// <summary>
    /// The User's Microsite Identity, described as UserName, UserGuid, and Microsite Root Guid
    /// </summary>
    public class MicrositeIdentity
    {
        /// <summary>
        /// The User's UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The User's UserGuid
        /// </summary>
        public string UserGuid { get; set; }
        /// <summary>
        /// The User's Microsite Root Guid
        /// </summary>
        public string MicrositeRootGuid { get; set; }
    }
}

