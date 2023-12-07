using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace KaiZai.Web.HttpAggregator.Grpc
{
    public static class MessageConverters
    {
        // Deserialize an Any type to a specific message type
        public static T DeserializeAny<T>(Any any) where T : IMessage<T>, new()
        {
            any.TryUnpack<T>(out var result);
            return result;
        }
    }
}