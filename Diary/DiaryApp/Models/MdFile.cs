using System;

namespace DiaryApp.Models
{
    public class MdFile
    {
        public string FileName { get; set; } = Guid.NewGuid().ToString();
        public string Note { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; 
    }
}