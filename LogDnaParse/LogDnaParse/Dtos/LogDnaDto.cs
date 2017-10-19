using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDnaParse.Dtos
{
    /// <summary>
    /// Main log class
    /// </summary>
    public class LogDnaDto
    {
        /// <summary>
        /// Primary object inside which most of the conditions lay
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MainOperatorsDto Primary { get; set; }

        /// <summary>
        /// original text passed in from URI
        /// </summary>
        public string OriginalContent { get; set; }
    }
}