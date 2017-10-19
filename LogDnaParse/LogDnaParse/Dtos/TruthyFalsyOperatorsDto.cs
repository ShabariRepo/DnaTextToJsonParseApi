using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDnaParse.Dtos
{
    public class TruthyFalsyOperatorsDto
    {
        /// <summary>
        /// Comparison will have $not
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Comparison { get; set; }

        /// <summary>
        /// boolean to show if its true or not
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool TruthyFalsy { get; set; }
    }
}