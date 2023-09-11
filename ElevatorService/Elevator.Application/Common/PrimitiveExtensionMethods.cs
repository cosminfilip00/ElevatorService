namespace Elevator.Application.Common;

public static class PrimitiveExtensionMethods
{
    public static string DisplayOrdinal(this int integerValue)
    {
        string number = integerValue.ToString();
        if (number.EndsWith("1")) return number + "st";
        if (number.EndsWith("2")) return number + "nd";
        if (number.EndsWith("3")) return number + "rd";
        if (number.EndsWith("11")) return number + "th";
        if (number.EndsWith("12")) return number + "th";
        if (number.EndsWith("13")) return number + "th";

        return number + "th";
    }
}

