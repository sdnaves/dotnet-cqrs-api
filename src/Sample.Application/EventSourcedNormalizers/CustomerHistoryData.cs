namespace Sample.Application.EventSourcedNormalizers
{
    public class CustomerHistoryData
    {
        private string _action = default!;
        private string _timestamp = default!;

        public string Action { get => _action; set => _action = value; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? BirthDate { get; set; }
        public string Timestamp { get => _timestamp; set => _timestamp = value; }
        public string? Who { get; set; }
    }
}
