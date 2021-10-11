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

    public static class TOUsernameContainerPathFormatterExtensions
    {
        public static TOUsernameContainerPathFormatter AddUserName(this TOUsernameContainerPathFormatter tObject, string value)
        {
            tObject.UserName = value;

            return tObject;
        }

        public static TOUsernameContainerPathFormatter AddContainer(this TOUsernameContainerPathFormatter tObject, string value)
        {
            tObject.Container = value;

            return tObject;
        }

        public static TOUsernameContainerPathFormatter AddPathFormatter(this TOUsernameContainerPathFormatter tObject, string value)
        {
            tObject.PathFormatter = value;

            return tObject;
        }

        public static TOUsernameContainerPathFormatter AddConnectionString(this TOUsernameContainerPathFormatter tObject, string value)
        {
            tObject.ConnectionString = value;

            return tObject;
        }
    }
}

