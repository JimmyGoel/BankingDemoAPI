using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class ApiError
    {
        public ApiError(int statuscode, string messages = null, string details = null)
        {
            this.StatusCode = statuscode;
            this.Messages = messages;
            this.Details = details;
        }

        public int StatusCode { get; set; }
        public string Messages { get; set; }
        public string Details { get; set; }

    }
}
