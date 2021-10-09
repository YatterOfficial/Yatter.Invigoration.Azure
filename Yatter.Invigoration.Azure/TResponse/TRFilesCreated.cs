using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRFilesCreated : ITResponse
    {
        public string TResponseType { get; set; }
        public bool Created { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TRFilesCreated()
        {
            TResponseType = GetType().ToString();
        }

    }
}

