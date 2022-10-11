using System;
using System.Net;
using InitManage.Helpers.Interfaces;
using InitManage.Models.Entities;
using Newtonsoft.Json;
using Simple.Http;

namespace InitManage.Helpers;

public class StackRequestHelper : IStackRequestHelper
{
    private string _jsonFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "stack.json");
    private bool _isStackIsRunning;
    private readonly IHttpService _httpService;

    public StackRequestHelper(IHttpService httpService)
    {
        _httpService = httpService;

        if (!File.Exists(_jsonFilePath))
            File.Create(_jsonFilePath);
    }

    public void AddItemToStack(StackItemEntity stackItem)
    {
        // 1) Add Item to stack
        var stackItems = GetFileContent();
        stackItems.ToList().Add(stackItem);


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
            var stackText = File.ReadAllText(_jsonFilePath);
            var stackItems = JsonConvert.DeserializeObject<IEnumerable<StackItemEntity>>(stackText);
            return stackItems;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return null;
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

