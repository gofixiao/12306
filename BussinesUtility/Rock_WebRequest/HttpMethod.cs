using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockUtility
{
    public static class HttpMethod
    {
        public enum RequestMethod
        {
            GET,
            POST,
            DELETE
        }

        /// <summary>
        /// Represent the HTTP method 'GET'.
        /// </summary>
        public const string Get = "GET";

        /// <summary>
        /// Represent the HTTP method 'POST'.
        /// </summary>
        public const string Post = "POST";

        /// <summary>
        /// Represent the HTTP method 'DELETE'.
        /// </summary>
        public const string Delete = "DELETE";
    }
}
