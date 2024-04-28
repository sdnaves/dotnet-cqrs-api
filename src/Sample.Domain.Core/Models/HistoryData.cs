namespace Sample.Domain.Core.Models
{
    public class HistoryData<T>
    {
        public string? Action { get; set; }
        public T? Data { get; set; }
        public string? Timestamp { get; set; }
        public string? Who { get; set; }
    }
}
