namespace Sample.Application.ViewModels
{
    public class HistoryDataViewModel<T> where T : class
    {
        public string Action { get; set; }
        public T Data { get; set; }
        public DateTime Timestamp { get; set; }
        public string Who { get; set; }
    }
}
