using System;
namespace Yatter.Invigoration.Azure.TObject
{
    public class TOWriteTextToBlobSettings : Yatter.Invigoration.ObjectBase
    {
        public string TObjectType { get; set; }

        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string BlobPath { get; set; }
        public string Content { get; set; }

        public TOWriteTextToBlobSettings()
        {
            TObjectType = GetType().ToString();
        }
    }

    public static class TOWriteTextToBlobSettingsExtensions
    {
        public static TOWriteTextToBlobSettings AddConnectionString(this TOWriteTextToBlobSettings tObject, string value)
        {
            tObject.ConnectionString = value;

            return tObject;
        }

        public static TOWriteTextToBlobSettings AddContainerName(this TOWriteTextToBlobSettings tObject, string value)
        {
            tObject.ContainerName = value;

            return tObject;
        }

        public static TOWriteTextToBlobSettings AddBlobPath(this TOWriteTextToBlobSettings tObject, string value)
        {
            tObject.BlobPath = value;

            return tObject;
        }

        public static TOWriteTextToBlobSettings AddContent(this TOWriteTextToBlobSettings tObject, string value)
        {
            tObject.Content = value;

            return tObject;
        }
    }
}


