using DiaryApp.Enums;
using System;
using System.Collections.Generic;

namespace DiaryApp.Models
{
    public class FileData
    {
        public DateTime DateAndTime { get; set; } = DateTime.Now;
        public string FileName { get; set; } = Guid.NewGuid().ToString() + ".day";
        public string Note { get; set; } = string.Empty;
        public WorkKind WorkKind { get; set; } = WorkKind.Undefined;
        public bool Trip { get; set; } = false;
        public string MarkdownContent { get; set; } = string.Empty;
        public bool FreeTimeActivity { get; set; } = false;
        public List<string> Tags { get; set; } = [];
    }
}
