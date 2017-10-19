using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDnaParse.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ComparisonOperatorsDto
    {
        /// <summary>
        /// Comparison will have $or, $and
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Comparison { get; set; }

        /// <summary>
        /// description for quotes and plain text passed in via URI
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Desc { get; set; }

        /// <summary>
        /// Value of the number greater than or less than
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// when using Len() the number within the brackets
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Length { get; set; }

        /// <summary>
        /// when comparing equals operator and value within the quotes
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Quoted { get; set; }
    }
}