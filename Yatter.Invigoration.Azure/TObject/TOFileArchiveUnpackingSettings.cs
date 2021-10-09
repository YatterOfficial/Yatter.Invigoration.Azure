using System;
using System.Collections.Generic;
using Yatter.Invigoration.Azure.Models;

namespace Yatter.Invigoration.Azure.TObject
{
    public class TOFileArchiveUnpackingSettings : Yatter.Invigoration.ObjectBase
    {
        public string ArchiveConnectionString { get; set; }
        public string ArchiveContainer { get; set; }
        public string ArchivePath { get; set; }
        public string UnpackingConnectionString { get; set; }
        public string UnpackingContainer { get; set; }
        public string UnpackingRootPath { get; set; }
        public List<SearchAndReplace> Substitutions { get; set; }
    }
}

