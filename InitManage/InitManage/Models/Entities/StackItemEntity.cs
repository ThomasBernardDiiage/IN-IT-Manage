using System;
namespace InitManage.Models.Entities;

public class StackItemEntity
{
    public int Priority { get; set; }
    public int NumberOfRetry { get; set; }
    public int CurrentRetry { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Content { get; set; }
    public HttpMethod HttpMethod { get; set;s }


    public StackItemEntity(string url, string content, HttpMethod httpMethod, int priority = 1, int numberOfRetry = 5)
    {
        Priority = priority;
        NumberOfRetry = numberOfRetry;
        Date = DateTime.Now;
        Url = url;
        Content = content;
        HttpMethod = httpMethod;
    }
}

