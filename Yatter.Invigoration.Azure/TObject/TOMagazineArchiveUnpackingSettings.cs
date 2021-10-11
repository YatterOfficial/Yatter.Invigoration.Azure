using System;
using System.Collections.Generic;
using Yatter.Invigoration.Azure.Models;

namespace Yatter.Invigoration.Azure.TObject
{
    public class TOMagazineArchiveUnpackingSettings : Yatter.Invigoration.ObjectBase
    {
        public string ArchiveConnectionString { get; set; }
        public string ArchiveContainer { get; set; }
        public string ArchivePath { get; set; }
        public string UnpackingConnectionString { get; set; }
        public string UnpackingContainer { get; set; }
        public string UnpackingRootPath { get; set; }
        public string UserName { get; set; }
        public string UserGuid { get; set; }
        public string MicrositeRootGuid { get; set; }
        public List<SearchAndReplace> Substitutions { get; set; }
    }

    public static class TOFileArchiveUnpackingSettingsExtensions
    {
        public static TOMagazineArchiveUnpackingSettings AddUserName(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.UserName = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddUserGuid(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.UserGuid = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddMicrositeRootGuid(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.MicrositeRootGuid = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddArchiveConnectionString(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.ArchiveConnectionString = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddArchiveContainer(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.ArchiveContainer = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddArchivePath(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.ArchivePath = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddUnpackingConnectionString(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.UnpackingConnectionString = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddUnpackingContainer(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.UnpackingContainer = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddUnpackingRootPath(this TOMagazineArchiveUnpackingSettings tObject, string value)
        {
            tObject.UnpackingRootPath = value;

            return tObject;
        }

        public static TOMagazineArchiveUnpackingSettings AddSubstitutions(this TOMagazineArchiveUnpackingSettings tObject, List<SearchAndReplace> value)
        {
            tObject.Substitutions = value;

            return tObject;
        }
    }
}

