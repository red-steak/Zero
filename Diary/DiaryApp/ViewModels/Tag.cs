namespace DiaryApp.ViewModels
{
    public class Tag
    {
        public Tag(string value, DiaryEntryViewModel entry)
        {
            Value = value;
            Entry = entry;
        }

        public string Value { get; set; } = string.Empty;
        public DiaryEntryViewModel Entry { get; set; }
        override public string ToString()
        {
            return Value;
        }
    }
}
