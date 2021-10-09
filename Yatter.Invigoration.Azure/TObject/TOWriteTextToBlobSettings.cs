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
}

