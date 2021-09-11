using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace CodeSnippetManagerGtk
{
    public class FileHandler
    {
        public List<Snippet> Files = new List<Snippet>();

        private string PathToSettingsSnippets
        {
            get
            {
                if (OperatingSystem.IsLinux())
                    return $"/home/{Environment.UserName}/.local/share/Snippets/snippets/";
                if (OperatingSystem.IsWindows())
                    return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Snippets/snippets/" ;
                return "";
            }
        }

        private string PathToSettings
        {
            get
            {
                if (OperatingSystem.IsLinux())
                    return $"/home/{Environment.UserName}/.local/share/Snippets/";
                if (OperatingSystem.IsWindows())
                    return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Snippets/" ;
                return "";
            }
        }

        public FileHandler()
        {

            Console.WriteLine(PathToSettingsSnippets);
            if (!Directory.Exists(PathToSettingsSnippets))
                Directory.CreateDirectory(PathToSettingsSnippets);
            else
                foreach (var file in Directory.EnumerateFiles(PathToSettingsSnippets))
                {
                    var name = Path.GetFileName(file);
                    var text = File.ReadAllText(file);
                    Files.Add(new Snippet(name, text));
                }
        }

        public void AddSnippet(Snippet snippet)
        {
            File.WriteAllText(PathToSettingsSnippets + snippet.FileName, snippet.Text);
        }

        public void RemoveSnippet(Snippet snippet)
        {
            File.Delete(PathToSettingsSnippets + snippet.FileName);
        }
    }
}