using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hurl.Library.Models
{
    public class Settings
    {
        [JsonInclude]
        public string Version = Constants.VERSION;

        [JsonInclude]
        public string LastUpdated { get; set; } = DateTime.Now.ToString();

        [JsonInclude]
        public List<Browser> Browsers;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AppSettings AppSettings { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public LinkPattern[] AutoRules { get; set; }
    }

    public class AppSettings
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool DisableAcrylic { get; set; } = false;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Byte> BackgroundRGB { get; set; } = new List<byte> { 51, 51, 51 };

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool LaunchUnderMouse { get; set; } = false;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool UseWhiteBorder { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string BackgroundType { get; set; } = "mica";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int[] WindowSize { get; set; } = new int[] { 420, 210 };
    }

    public class LinkPattern
    {
        public string Pattern { get; set; }
        public Browser Browser { get; set; }
    }
}
