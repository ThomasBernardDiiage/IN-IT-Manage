using System;
namespace InitManage.Commons;

public static class DateTimeExtension
{
    public static bool IsBetween(this DateTime dateTime, DateTime rangeBeggin, DateTime rangeEnd)
    {
        return dateTime.Ticks >= rangeBeggin.Ticks && dateTime.Ticks <= rangeEnd.Ticks;
    }
}

