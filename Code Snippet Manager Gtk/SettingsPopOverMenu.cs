using Gtk;

namespace CodeSnippetManagerGtk
{
    public class SettingPopOverMenu : PopoverMenu
    {
        public SettingPopOverMenu()
        {
            var appSettings = new AppSettings();
            var settingsData = appSettings.GetSettings();


            var darkMode = new CheckButton();
            darkMode.Label = "Dark Mode";
            darkMode.Active = settingsData.DarkModeEnabled;
            darkMode.Toggled += (sender, args) =>
            {
                settingsData.DarkModeEnabled = !settingsData.DarkModeEnabled;
                Settings.Default.ApplicationPreferDarkTheme = settingsData.DarkModeEnabled;
                appSettings.SetSettings(settingsData);
            };


            var vBox = new VBox();
            vBox.Add(darkMode);
            vBox.ShowAll();
            vBox.Margin = 12;
            Add(vBox);
        }
    }
}