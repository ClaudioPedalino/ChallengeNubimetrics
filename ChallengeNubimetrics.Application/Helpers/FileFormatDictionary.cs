using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Helpers
{
    public static class FileFormatDictionary
    {
        public static Dictionary<FileFormat, string> FileFormats { get; set; } = new Dictionary<FileFormat, string>()
        {
            {FileFormat.JSON, ".json" } ,
            {FileFormat.CSV, ".csv" }
        };
    }

    public enum FileFormat
    {
        JSON = 1,
        CSV = 2
    }

}
