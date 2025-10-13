using System.Globalization;

namespace DoNet.SharedKernel.Extensions;

public static class DateTimeOffsetExtensions
{
    public static string? ToShamsiOffset(this DateTimeOffset? date)
    {
        if (date == null)
            return null;

        var persian = new PersianCalendar();
        var dt = date.Value.DateTime;

        return string.Format("{0}/{1:D2}/{2:D2}",
            persian.GetYear(dt),
            persian.GetMonth(dt),
            persian.GetDayOfMonth(dt));
    }

    public static string ToShamsiOffset(this DateTimeOffset date)
    {
        var persian = new PersianCalendar();
        var dt = date.DateTime;

        return string.Format("{0}/{1:D2}/{2:D2}",
            persian.GetYear(dt),
            persian.GetMonth(dt),
            persian.GetDayOfMonth(dt));
    }
}
