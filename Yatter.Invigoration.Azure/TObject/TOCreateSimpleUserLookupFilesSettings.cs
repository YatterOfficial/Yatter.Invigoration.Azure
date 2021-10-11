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
        public string UserGuid { get; set; }
        public string UserRootGuid { get; set; }
        public string UserFilePath { get; set; }
        public string UserGuidFilePath { get; set; }
        public SimpleUserNameFileContent SimpleUserNameFileContent { get; set; }
        public SimpleUserGuidFileContent SimpleUserGuidFileContent { get; set; }

        public TOCreateSimpleUserLookupFilesSettings()
        {
            TObjectType = GetType().ToString();
        }
    }

    public static class TOCreateSimpleUserLookupFilesSettingsExtensions
    {
        public static TOCreateSimpleUserLookupFilesSettings AddConnectionString(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.ConnectionString = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddContainerName(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.ContainerName = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddUserName(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.UserName = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddUserGuid(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.UserGuid = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddUserRootGuid(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.UserRootGuid = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddUserFilePath(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.UserFilePath = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddUserGuidFilePath(this TOCreateSimpleUserLookupFilesSettings tObject, string value)
        {
            tObject.UserGuidFilePath = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddSimpleUserNameFileContent(this TOCreateSimpleUserLookupFilesSettings tObject, SimpleUserNameFileContent value)
        {
            tObject.SimpleUserNameFileContent = value;

            return tObject;
        }

        public static TOCreateSimpleUserLookupFilesSettings AddSimpleUserGuidFileContent(this TOCreateSimpleUserLookupFilesSettings tObject, SimpleUserGuidFileContent value)
        {
            tObject.SimpleUserGuidFileContent = value;

            return tObject;
        }
    }
}

