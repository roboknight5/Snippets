using System;
using System.Collections.Generic;
using GLib;
using Gtk;

namespace CodeSnippetManagerGtk
{
    public class ItemSelection : MenuButton
    {
        private readonly ListBox _listBox = new ListBox();
        private int _count = 0;
        private readonly List<Snippet> _listedItems = new List<Snippet>();
        private readonly Program _parent;
        private readonly FileHandler _fileHandler;
        private readonly PopoverMenu _popoverMenu;

        public ItemSelection(Program parent)
        {

            
            parent.Entry.Changed += (o, args) =>
            {
                if (parent.Entry.Text == "")
                {
                    parent.SourceView.Buffer.Text = "";
                    Label = "";
                }
            };

            Image = Gtk.Image.NewFromIconName("folder-symbolic", IconSize.Button);
            AlwaysShowImage = true;

            this._parent = parent;
            _fileHandler = new FileHandler();

            _popoverMenu = new PopoverMenu();
            var vBox = new VBox();

            Console.WriteLine(_fileHandler.Files.Count);

            foreach (var snippet in _fileHandler.Files)
                AddSnippetUi(snippet);
            
            Realized+=(sender, args) =>
            {
                    Visible = _count!=0;
                
            };


            Label = "";
            

            _listBox.ShowAll();

            var scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(_listBox);

            vBox.PackStart(scrolledWindow, true, true, 5);

            vBox.ShowAll();
            vBox.Margin = 12;
            _popoverMenu.Add(vBox);
            _popoverMenu.Expand = true;
            Popover = _popoverMenu;

 




        }

        public void AddItem(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return;

            foreach (var s in _listedItems)
                if (s.FileName == name)
                {
                    Label = name;
                    s.Text = _parent.SourceView.Buffer.Text;
                    _fileHandler.AddSnippet(s);
                    return;
                }

            var snippet = new Snippet(name, _parent.SourceView.Buffer.Text);

            AddSnippetUi(snippet);
            _fileHandler.AddSnippet(snippet);
        }

        private void AddSnippetUi(Snippet snippet)
        {
            _listedItems.Add(snippet);
            _count++;
            if (_count != 0) Visible = true;
            Label = snippet.FileName;
            var eventBox = new EventBox();
            var menuItem = new ListItem(snippet.FileName);

            var listBoxRow = new ListBoxRow();
            eventBox.Add(menuItem);

            listBoxRow.Child = eventBox;

            _listBox.Add(listBoxRow);


            eventBox.ButtonPressEvent += (o, args) =>
            {
                Label = menuItem.Label.Text;
                _parent.Entry.Text = Label;
                _parent.SourceView.Buffer.Text = snippet.Text;
                _fileHandler.AddSnippet(snippet);
            };
            eventBox.ButtonReleaseEvent += (o, args) => _popoverMenu.Hide();


            menuItem.Button.Clicked += (o, args) =>
            {
                _count--;
                eventBox.Remove(menuItem);
                _listBox.Remove(listBoxRow);
                _listedItems.Remove(snippet);
                _fileHandler.RemoveSnippet(snippet);
                if (snippet.FileName == _parent.Entry.Text)
                {
                    _parent.Entry.Text = "";
                    Label = "";
                }

                if (_count == 0)
                {
                    Visible = false;
                    _parent.SourceView.Buffer.Text = "";
                    _parent.Entry.Text = "";
                }
            };

            _listBox.ShowAll();
        }
    }
}