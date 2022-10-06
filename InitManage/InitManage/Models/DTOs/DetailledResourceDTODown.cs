using System;
using System.Collections;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs
{
    public class DetailledResourceDTODown : ResourceDTODown
    {
        public DetailledResourceDTODown()
        {
        }

        [JsonProperty("options")]
        public IEnumerable<OptionDTO> Options { get; set;}

        [JsonProperty("booking")]
        public IEnumerable<BookingDTO> Bookings { get; set; }
    }
}

