namespace CodeSnippetManagerGtk
{
    public class Snippet
    {
        public string FileName { get; set; }
        public string Text { get; set; }

        public Snippet(string fileName, string text)
        {
            FileName = fileName;
            Text = text;
        }
    }
}