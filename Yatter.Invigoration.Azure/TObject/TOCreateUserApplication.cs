using System;
namespace Yatter.Invigoration.Azure.TObject
{
    public class TOCreateUserApplicationData : Yatter.Invigoration.ObjectBase
    {
        public string TObjectType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string MobileCountryCode { get; set; }
        public string MobileNumber { get; set; }

        public TOCreateUserApplicationData()
        {
            TObjectType = GetType().ToString();
        }


    }
}

