using System;
using InitManage.Models.Interfaces;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs
{
    public class BookingDTO : IBookingEntity
    {
        public BookingDTO()
        {
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("resourceId")]
        public long ResourceId { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
}

