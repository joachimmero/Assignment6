using System;
 
namespace ApiTeht
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
 
        public LogEntry(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
        }
    }
}