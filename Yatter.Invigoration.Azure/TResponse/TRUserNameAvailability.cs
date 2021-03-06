using System;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TResponse
{
    public class TRUserNameAvailability : ITResponse
    {
        public string TResponseType { get; set; }
        public bool IsAvailable { get; set; }
        public string UserName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TRUserNameAvailability()
        {
            TResponseType = GetType().ToString();
        }
    }
}

