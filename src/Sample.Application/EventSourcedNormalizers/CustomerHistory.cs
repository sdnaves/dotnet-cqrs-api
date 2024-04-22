using MongoDB.Bson;
using Newtonsoft.Json;
using Sample.Domain.Core.Models;

namespace Sample.Application.EventSourcedNormalizers
{
    public static class CustomerHistory
    {
        public static IList<CustomerHistoryData> HistoryData { get; set; } = [];

        public static IList<CustomerHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = [];

            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<CustomerHistoryData>();
            var last = new CustomerHistoryData();

            foreach (var change in sorted)
            {
                var historyData = new CustomerHistoryData
                {
                    Id = change.Id == ObjectId.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                        ? ""
                        : change.BirthDate.Substring(0, 10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(historyData);
                last = change;
            }

            return list;
        }

        private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonConvert.DeserializeObject<CustomerHistoryData>(e.Data);

                if (historyData is null)
                    continue;

                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "CustomerCreatedEvent":
                        historyData.Action = "Created";
                        historyData.Who = e.User;
                        break;
                    case "CustomerUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "CustomerDeletedEvent":
                        historyData.Action = "Deleted";
                        historyData.Who = e.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = e.User ?? "Anonymous";
                        break;

                }

                HistoryData.Add(historyData);
            }
        }
    }
}
