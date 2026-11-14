using DiaryApp.Enums;

namespace DiaryApp.ViewModels
{
    public class WorkKindItem
    {
        public string Name { get; set; }
        public WorkKind Value { get; set; }
        public WorkKindItem(string name, WorkKind value)
        {
            Name = name;
            Value = value;
        }
    }
}
