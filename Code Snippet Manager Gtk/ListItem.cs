using Gtk;

namespace CodeSnippetManagerGtk
{
    public class ListItem : HBox
    {
        public Label Label { get; set; }
        public Button Button { get; set; }

        public ListItem(string str)
        {
            var image = Image.NewFromIconName("folder-symbolic", IconSize.Button);
            Label = new Label(str);
            Button = new Button {Image = Image.NewFromIconName("user-trash-symbolic", IconSize.Button)};
            PackStart(image, false, false, 5);
            PackStart(Label, false, false, 5);
            PackEnd(Button, false, false, 5);
            MarginEnd = 15;
            ShowAll();
        }
    }
}