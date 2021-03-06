using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRWriteBlobResponse : ITResponse
    {
        public string TResponseType { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TRWriteBlobResponse()
        {
            TResponseType = GetType().ToString();
        }
    }
}

