using System;
namespace Yatter.Invigoration.Azure.TObject.TObject
{
    public class TOUsernameContainerPathFormatter : Yatter.Invigoration.ObjectBase
    {
        public string TObjectType { get; set; }

        public string UserName { get; set; }
        public string Container { get; set; }
        public string PathFormatter { get; set; }
        public string ConnectionString { get; set; }

        public TOUsernameContainerPathFormatter()
        {
            TObjectType = GetType().ToString();
        }
    }
}

