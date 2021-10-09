using System;
namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRFatalResponse : ITResponse
    {
        public string TResponseType { get; set; }
        public string Message { get; set; }

        public TRFatalResponse()
        {
            TResponseType = GetType().ToString();
        }
    }
}

