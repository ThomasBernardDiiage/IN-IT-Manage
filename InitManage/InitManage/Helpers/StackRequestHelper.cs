using System;
using System.Net;
using InitManage.Helpers.Interfaces;
using InitManage.Models.Entities;
using Newtonsoft.Json;
using Simple.Http;

namespace InitManage.Helpers;

public class StackRequestHelper : IStackRequestHelper
{
    private string _jsonFilePath = "stack.json";
    private bool _isStackIsRunning;
    private readonly IHttpService _httpService;

    public StackRequestHelper(IHttpService httpService)
    {
        if (!File.Exists(_jsonFilePath))
            File.Create(_jsonFilePath);
        _httpService = httpService;
    }

    public void AddItemToStack(StackItemEntity stackItem)
    {
        // 1) Add Item to stack
        var stackItems = GetFileContent()?.ToList();

        // If stackItems is null or empty
        if (stackItems == null)
            stackItems = new List<StackItemEntity>();

        stackItems.Add(stackItem);

        WriteFileContent(JsonConvert.SerializeObject(stackItems));

        // 2) Start stack if not started

        if (!_isStackIsRunning)
        {
            _isStackIsRunning = true;
            var stackThread = new Thread(StartStack);
            stackThread.Start();
        }
    }


    private async void StartStack()
    {

        // Get all item from json file
        var stack = GetFileContent()
            .OrderBy(stackItem => stackItem.Priority)
            .OrderBy(stackItem => stackItem.Date)
            .ToList();

        while (_isStackIsRunning)
        {
            foreach (var stackItem in stack)
            {
                var response = await _httpService.SendRequestAsync(stackItem.Url, stackItem.HttpMethod, stackItem.Content);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    stack.Remove(stackItem);
                }
                else
                {
                    // If current retrty >= of maxnumberof retry
                    stackItem.NumberOfRetry++;
                    if (stackItem.CurrentRetry >= stackItem.NumberOfRetry)
                    {
                        Console.WriteLine("==== ERROR ====");
                        Console.WriteLine($"Error when sending request : {stackItem.Url} {stackItem.HttpMethod} {stackItem.Content}");
                        Console.WriteLine("==== ==== ==== ====");
                        stack.Remove(stackItem);
                    }
                }


            }
        }



        _isStackIsRunning = false;
    }

    private IEnumerable<StackItemEntity> GetFileContent()
    {
        try
        {
            var stream = FileSystem.OpenAppPackageFileAsync(_jsonFilePath).GetAwaiter().GetResult();
            var reader = new StreamReader(stream);

            var stackItems = JsonConvert.DeserializeObject<IEnumerable<StackItemEntity>>(reader.ReadToEnd());
            return stackItems;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void WriteFileContent(string content)
    {
        try
        {
            var stream = FileSystem.OpenAppPackageFileAsync(_jsonFilePath).GetAwaiter().GetResult();
            var sw = new StreamWriter(stream);
            sw.Write(content);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }



    //private void CheckIfFileIsCreated()
    //{
    //    try
    //    {
    //        if (!File.Exists("stack.json"))
    //            File.Create("stack.json");
    //    }
    //    catch(Exception ex)
    //    {
    //        Console.WriteLine(ex);
    //    }
    //}
}

