using System;
using System.IO;
using System.Text.Json;

namespace CodeSnippetManagerGtk
{
    public class SettingsData
    {
        public bool DarkModeEnabled { get; set; } = true;
    }

    public class AppSettings
    {
        private string PathToSettings
        {
            get
            {
                if (OperatingSystem.IsLinux())
                    return $"/home/{Environment.UserName}/.local/share/Snippets/";
                if (OperatingSystem.IsWindows())
                    return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Snippets/snippets" ;
                return "";
            }
        }

        private readonly string _settingsName = "settings.json";

        public AppSettings()
        {
            if (!Directory.Exists(PathToSettings))
            {
                Directory.CreateDirectory(PathToSettings);
            }
            else
            {
                if (!File.Exists(PathToSettings + _settingsName))
                {
                    var jsonString = JsonSerializer.Serialize(new SettingsData());
                    File.WriteAllText(PathToSettings + _settingsName, jsonString);
                }
            }
        }

        public SettingsData GetSettings()
        {
            var jsonString = File.ReadAllText(PathToSettings + _settingsName);
            var data = JsonSerializer.Deserialize<SettingsData>(jsonString);
            return data;
        }

        public void SetSettings(SettingsData settingsData)
        {
            var jsonString = JsonSerializer.Serialize(settingsData);
            File.WriteAllText(PathToSettings + _settingsName, jsonString);
        }
    }
}