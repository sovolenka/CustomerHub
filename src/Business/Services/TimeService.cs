﻿namespace Business.Services;

public class TimeService
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }
    
    public static DateTime? DateOnlyToDateTime(DateOnly? dateOnly)
    {
        if (dateOnly == null)
        {
            return null;
        }
        return new DateTime(dateOnly.Value.Year, dateOnly.Value.Month, dateOnly.Value.Day);
    }
}