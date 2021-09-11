using System;
using System.IO;
using System.Net.Mime;
using Gdk;
using Gtk;
using GtkSource;
using Window = Gtk.Window;

namespace CodeSnippetManagerGtk
{
    public class Program : Window
    {
        public SourceView SourceView { get; }
        public SearchEntry Entry { get; }

        private static void Main(string[] args)
        {
            Gtk.Application.Init();
            new Program("Snippets");
            Gtk.Application.Run();
        }

        private Program(string title) : base(title)
        {
            DeleteEvent += delegate { Gtk.Application.Quit(); };
            SetDefaultSize(600, 600);

            try
            {
                SetIconFromFile("/usr/share/icons/Gtk-Code-Snippet-Manager-icon.png");
            }
            catch 
            {
                try
                {
                    SetIconFromFile("Gtk-Code-Snippet-Manager-icon.png");


                }
                catch 
                {

                }
            }

            var appSettings = new AppSettings();
            Settings.Default.ApplicationPreferDarkTheme = appSettings.GetSettings().DarkModeEnabled;

            var headerBar = new HeaderBar {Title = title, ShowCloseButton = true, Visible = true};

            var settingPopOverMenu = new SettingPopOverMenu();

            var themeButton = new MenuButton
            {
                Image = Gtk.Image.NewFromIconName("open-menu-symbolic", IconSize.LargeToolbar),
                Visible = true,
                Popover = settingPopOverMenu
            };


            Pixbuf pixbuf = null;
            try
            {
                pixbuf = new Pixbuf("/usr/share/icons/Gtk-Code-Snippet-Manager-icon.png");
                pixbuf = pixbuf.ScaleSimple(32, 32, InterpType.Bilinear);
            }
            catch  
            {
                try
                {
                    pixbuf = new Pixbuf("Gtk-Code-Snippet-Manager-icon.png");
                    pixbuf = pixbuf.ScaleSimple(32, 32, InterpType.Bilinear);
                }
                catch 
                {
               

                }
            }

            var image = new Gtk.Image {Pixbuf = pixbuf, Visible = true};

            headerBar.PackStart(image);
            headerBar.PackEnd(themeButton);
            Titlebar = headerBar;


            var vBox = new VBox();

            SourceView = new SourceView {WrapMode = WrapMode.Word};
            var textScrolledWindow = new ScrolledWindow();
            textScrolledWindow.Add(SourceView);

            var hBox = new HBox();


            Entry = new SearchEntry();
            var itemSelection = new ItemSelection(this);

            var button = new Button();

            button.Clicked += (sender, args) =>
            {
                var text = Entry.Text;
                itemSelection.AddItem(text);
            };

            button.Image = Gtk.Image.NewFromIconName("tool-pencil", IconSize.Button);

            hBox.PackStart(itemSelection, false, false, 5);
            hBox.PackStart(Entry, true, true, 5);
            hBox.PackStart(button, false, false, 5);

            vBox.PackStart(hBox, false, false, 5);
            vBox.PackStart(textScrolledWindow, true, true, 5);
            vBox.Margin = 12;

            Add(vBox);
            vBox.ShowAll();
            ShowAll();
        }
    }
}