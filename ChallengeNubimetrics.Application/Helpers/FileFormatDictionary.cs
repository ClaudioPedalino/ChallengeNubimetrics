using ChallengeNubimetrics.Application.Helpers.Enum;
using System.Collections.Generic;

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
}