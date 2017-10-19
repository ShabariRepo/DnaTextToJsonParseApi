using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDnaParse.Dtos
{
    /// <summary>
    /// Main Operators class consists of the and or and not objects in JSON
    /// </summary>
    public class MainOperatorsDto
    {
        /// <summary>
        /// and Object in JSON
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] And { get; set; }

        /// <summary>
        /// or OBject in JSON
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] Or { get; set; }

        /// <summary>
        /// not Object in JSON
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] Not { get; set; }
    }
}