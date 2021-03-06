﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using DarrenTestProject.Handlers;
using DarrenTestProject.Models;
using HandlerTemplates.Filters;

namespace DarrenTestProject.Controllers
{
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        // GET: api/Values
        [HttpGet, Route("")]
        //[Authorize] // -> was a proper IPrincipal set?
        //[OverrideAuthentication]
        //[JwtAuthenticationFilter]
        public IEnumerable<string> Get()
        {
            //var getValueByIdUrl = Url.Link("GetValueById", new { id = 123 });

            ////return new string[] { "value1", "value2", Request.GetApiKey() };
            //return new String[]
            //{
            //    getValueByIdUrl,
            //    Request.GetSelfReferenceBaseUrl().ToString(),
            //    Request.RebaseUrlForClient(new Uri(getValueByIdUrl)).ToString(),
            //    Request.GetClientIpAddress()
            //};

            return new string[] { User.Identity.Name, User.Identity.AuthenticationType };
        }

        // GET: api/Values/5
        [HttpGet, Route("{id:int}", Name = "GetValueById")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put([FromUri]int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }

        // example base64 binary data, converts to a sample json object:  
        // ew0KICAibnVtYmVyIjogMTIzLA0KICAib2JqZWN0Ijogew0KICAgICJhIjogImIiLA0KICAgICJjIjogImQiLA0KICAgICJlIjogImYiDQogIH0sDQogICJzdHJpbmciOiAiSGVsbG8gV29ybGQiDQp9
        [HttpGet, Route("binary/{array:base64:maxlength(512)}")]
        public string GetBinaryArray([ModelBinder(typeof(Base64ParameterModelBinder))]byte[] array)
        {
            return System.Text.Encoding.UTF8.GetString(array);

        }

        [HttpGet, Route("complex")]
        public IHttpActionResult ComplexTest([FromUri]ComplexTypeDto obj)
        {
            return Json(obj);
        }

        [HttpPut, Route("bodytest")]    
        public string BodyTest([FromBody] string value)
        {
            return value;
        }

        [HttpGet, Route("dates/{*myDate:datetime}")]
        public string GetDate(DateTime myDate)
        {
            return myDate.ToLongDateString();
        }

        [HttpGet, Route("segments/{*array:maxlength(256)}")]
        public string[] GetSegments([ModelBinder(typeof(StringArrayWildcardBinder))] string[] array)
        {
            return array;
        }
    }
}
