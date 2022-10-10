using System;
using InitManage.Models.Entities;

namespace InitManage.Helpers.Interfaces;

public interface IStackRequestHelper
{
    /// <summary>
    /// Add Item To Stack and start the stack
    /// </summary>
    /// <param name="httpRequestMessage"></param>
    /// <param name="numberOfRetry"></param>
    void AddItemToStack(StackItemEntity stackItem);
}

