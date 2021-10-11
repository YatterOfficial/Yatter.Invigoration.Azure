using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRCreateSimpleUserLookupFilesResponse : ITResponse
    {
        public string TResponseType { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string UserName { get; set; }
        public string UserGuid { get; set; }
        public string UserRootGuid { get; set; }

        public TRCreateSimpleUserLookupFilesResponse()
        {
            TResponseType = GetType().ToString();
        }
    }
}

