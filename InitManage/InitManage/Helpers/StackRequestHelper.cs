using System;
using InitManage.Helpers.Interfaces;
using InitManage.Models.Entities;
using Newtonsoft.Json;

namespace InitManage.Helpers;

public class StackRequestHelper : IStackRequestHelper
{

    private bool _isStackIsRunning;

    public StackRequestHelper()
    {
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


    private void StartStack()
    {
        var stackText = GetFileContent();

        _isStackIsRunning = false;
    }

    private IEnumerable<StackItemEntity> GetFileContent()
    {
        try
        {
            var stackText = File.ReadAllText("stack.json");
            var stackItems = JsonConvert.DeserializeObject<IEnumerable<StackItemEntity>>(stackText);
            return stackItems;
        }
        catch(Exception ex)
        {

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

