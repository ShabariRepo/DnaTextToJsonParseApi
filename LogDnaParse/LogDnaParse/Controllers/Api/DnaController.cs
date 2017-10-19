using LogDnaParse.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace LogDnaParse.Controllers.Api
{
    public class DnaController : ApiController
    {
        private IParseTextToJson _parseTextToJson;

        public DnaController() => _parseTextToJson = new ParseTextToJson();

        public DnaController(IParseTextToJson parse)
        {
            _parseTextToJson = parse;
        }

        /// <summary>
        /// GET api/dna?text=error;or;info;
        /// the text will then be split and parsed based on each section
        /// </summary>
        /// <param name="text">Text from the URI main components separated by ; </param>
        /// <returns>JSON result object modelled from the LogDnaDto class</returns>
        public IHttpActionResult Get([FromUri]string text)
        {
            var result = _parseTextToJson.ParseText(text);

            // can use JSON serializer to add any extra details like date formatting etc
            return new JsonResult<object>(result, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
