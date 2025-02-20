﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgLocaleCore.Utils;

public static class DtUtils
{
    #region Public and private methods - Core

    /// <summary>
    /// Get DateTime from unix long.
    /// </summary>
    /// <param name="unixDate"></param>
    /// <returns></returns>
    public static DateTime CastLongAsDtOldStyle(long unixDate)
    {
        DateTime dt = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dt = dt.AddSeconds(unixDate).ToLocalTime();
        return dt;
    }

    public static long CastDtAsLong(DateTime dt) => dt.Ticks;

    public static long CastAsLong(this DateTime dt) => dt.Ticks;

    public static DateTime CastLongAsDt(long ticks) => new(ticks);

    public static DateTime CastAsDt(this long ticks) => new(ticks);

    #endregion
}