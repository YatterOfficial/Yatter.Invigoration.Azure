using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRBlobContent : ITResponse
    {
        public string TResponseType { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public string Content { get; set; }

        public TRBlobContent()
        {
            TResponseType = GetType().ToString();
        }
    }
}

