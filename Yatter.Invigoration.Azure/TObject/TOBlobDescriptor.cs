using System;
namespace Yatter.Invigoration.Azure.TObject
{
    public class TOBlobDescriptor : Yatter.Invigoration.ObjectBase
    {
        public string TObjectType { get; set; }

        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string BlobPath { get; set; }

        public TOBlobDescriptor()
        {
            TObjectType = GetType().ToString();
        }
    }

    public static class TOBlobDescriptorExtensions
    {
        public static TOBlobDescriptor AddConnectionString(this TOBlobDescriptor tObject, string value)
        {
            tObject.ConnectionString = value;

            return tObject;
        }

        public static TOBlobDescriptor AddContainerName(this TOBlobDescriptor tObject, string value)
        {
            tObject.ContainerName = value;

            return tObject;
        }

        public static TOBlobDescriptor AddBlobPath(this TOBlobDescriptor tObject, string value)
        {
            tObject.BlobPath = value;

            return tObject;
        }
    }
}

