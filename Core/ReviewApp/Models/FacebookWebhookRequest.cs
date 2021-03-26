using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReviewApp.Models
{
    public class FacebookWebhookRequest
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("entry")]
        public IEnumerable<Entry> Entry  {get; set; }
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("messaging")]
        public IEnumerable<Messaging> Messaging { get; set; }
    }

    public class Messaging
    {
        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }

    public class Sender
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Message
    {
        [JsonProperty("mid")]
        public string MessageId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
