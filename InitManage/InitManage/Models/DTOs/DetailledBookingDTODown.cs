using System;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs;

public class DetailledBookingDTODown
{
    public DetailledBookingDTODown()
    {
    }

    [JsonProperty("IdBooking")]
    public int IdBooking { get; set; }

    [JsonProperty("Firstname")]
    public string Firstname { get; set; }

    [JsonProperty("Lastname")]
    public string Lastname { get; set; }

    [JsonProperty("Start")]
    public DateTime Start { get; set; }

    [JsonProperty("End")]
    public DateTime End { get; set; }

    [JsonProperty("Capacity")]
    public int Capacity { get; set; }

    [JsonProperty("IdResource")]
    public int IdResource { get; set; }

    [JsonProperty("ResourceName")]
    public string ResourceName { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }

    [JsonProperty("Picture")]
    public string Picture { get; set; }

    [JsonProperty("MaxCapacity")]
    public int MaxCapacity { get; set; }

    [JsonProperty("Position")]
    public string Position { get; set; }
}

