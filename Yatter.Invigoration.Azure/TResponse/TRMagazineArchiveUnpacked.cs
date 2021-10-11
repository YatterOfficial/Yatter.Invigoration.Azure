using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRMagazineArchiveUnpacked : ITResponse
    {
        public string TResponseType { get; set; }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public TRMagazineArchiveUnpacked()
        {
            TResponseType = GetType().ToString();
        }
    }
}

