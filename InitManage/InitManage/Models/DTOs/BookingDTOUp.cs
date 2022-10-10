using InitManage.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitManage.Models.DTOs
{
    public class BookingDTOUp
    {
        [JsonProperty("resourceId")]
        public long ResourceId { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        public BookingDTOUp()
        {
        }

        public BookingDTOUp(IBookingEntity booking)
        {
            ResourceId = booking.ResourceId;
            Start = booking.Start.ToString("yyyy-MM-dd hh:mm:ss");
            End = booking.End.ToString("yyyy-MM-dd hh:mm:ss");
            Capacity = booking.Capacity;
        }
    }
}
