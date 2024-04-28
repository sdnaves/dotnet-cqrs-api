using Newtonsoft.Json;
using Sample.Application.Interfaces;
using Sample.Domain.Core.Models;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;

namespace Sample.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public HistoryService(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IList<HistoryData<T>>> GetHistoryData<T>(string aggregatedId) where T : new()
        {
            var events = await _eventStoreRepository.GetAllAsync(aggregatedId);

            var historyDataDeserialized = HistoryDataDeserialize<T>(events);

            var sorted = historyDataDeserialized.OrderBy(c => c.Timestamp);
            var list = new List<HistoryData<T>>();
            var last = new T();

            foreach (var item in sorted)
            {
                var historyData = new HistoryData<T>()
                {
                    Action = string.IsNullOrWhiteSpace(item.Action) ? "" : item.Action,
                    Timestamp = item.Timestamp,
                    Who = item.Who,
                };

                var properties = typeof(T).GetProperties();

                var tObject = new T();

                foreach (var property in properties)
                {
                    var actualValue = property.GetValue(item.Data);
                    var lastValue = property.GetValue(last);

                    var value = actualValue?.Equals(lastValue) ?? true ? default : actualValue;

                    property.SetValue(tObject, value);
                }

                historyData.Data = tObject;

                list.Add(historyData);
                last = item.Data;
            }

            return list;
        }

        public static IList<HistoryData<T>> HistoryDataDeserialize<T>(IList<StoredEvent> events)
        {
            var historyDataDeserialized = new List<HistoryData<T>>();

            foreach (var @event in events)
            {
                var eventData = JsonConvert.DeserializeObject<T>(@event.Data);

                if (eventData is null)
                    continue;

                var historyData = new HistoryData<T>
                {
                    Data = eventData,
                    Timestamp = @event.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fff")
                };

                switch (@event.MessageType)
                {
                    case string a when a.Contains("Created"):
                        historyData.Action = "Created";
                        historyData.Who = @event.User;
                        break;

                    case string b when b.Contains("Updated"):
                        historyData.Action = "Updated";
                        historyData.Who = @event.User;
                        break;

                    case string c when c.Contains("Deleted"):
                        historyData.Action = "Deleted";
                        historyData.Who = @event.User;
                        break;

                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = @event.User ?? "Anonymous";
                        break;
                }

                historyDataDeserialized.Add(historyData);
            }

            return historyDataDeserialized;
        }
    }
}
