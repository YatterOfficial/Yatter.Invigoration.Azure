using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRBlobExists : ITResponse
    {
        public string TResponseType { get; set; }
        public bool Exists { get; set; }
        public string Container { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public TRBlobExists()
        {
            TResponseType = GetType().ToString();
        }
    }
}

