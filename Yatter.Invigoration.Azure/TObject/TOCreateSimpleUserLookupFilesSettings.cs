using System;
using Yatter.Invigoration.Azure.Models;

namespace Yatter.Invigoration.Azure.TObject
{
    public class TOCreateSimpleUserLookupFilesSettings : Yatter.Invigoration.ObjectBase
    {
        public string TObjectType { get; set; }

        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string UserName { get; set; }
        public string UserFilePath { get; set; }
        public string UserGuidFilePath { get; set; }
        public SimpleUserNameFileContent SimpleUserNameFileContent { get; set; }
        public SimpleUserGuidFileContent SimpleUserGuidFileContent { get; set; }

        public TOCreateSimpleUserLookupFilesSettings()
        {
            TObjectType = GetType().ToString();
        }
    }
}

