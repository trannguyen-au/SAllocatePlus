using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tna.SAllocatePlus.CommonShared
{
    public class JsonResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public JsonResponseMessage()
        {
            IsSuccess = true;
            Message = "";
        }

        public JsonResponseMessage Success(string message, object data = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            return this;
        }

        public JsonResponseMessage Error(string message, object data = null)
        {
            IsSuccess = false;
            Message = message;
            Data = data;
            return this;
        }
    }
}