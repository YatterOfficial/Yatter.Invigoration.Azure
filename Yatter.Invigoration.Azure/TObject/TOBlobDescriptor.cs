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
}

