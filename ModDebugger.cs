using System.Diagnostics;

/// <summary>
/// A wrapper class for <see cref="Debugger"/> that does not get flagged in K#.
/// 
/// Just place this class in your project then use ModDebugger anywhere you would have used <see cref="Debugger"/>
/// </summary>
static class ModDebugger
{
    /// <summary>
    /// Returns true if the preprocessor variable MOD_DEBUGGER_ACTIVE is defined.
    /// </summary>
    public static bool ModDebuggerActive
    {
        get
        {
#if MOD_DEBUGGER_ACTIVE
                return true;
#else
            return false;
#endif
        }
    }

    #region Proxy Methods

    /// <summary>
    /// Is a proxy with the same value as <see cref="Debugger.DefaultCategory">Debugger.DefaultCategory</see>.
    /// </summary>
#if MOD_DEBUGGER_ACTIVE
        public static readonly string DefaultCategory = Debugger.DefaultCategory;
#else
    public static readonly string DefaultCategory = "";
#endif

    /// <summary>
    /// Calls <see cref="Debugger.IsAttached">Debugger.IsAttached</see>.
    /// </summary>
    public static bool IsAttached
    {
        get
        {
#if MOD_DEBUGGER_ACTIVE
                return Debugger.IsAttached;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// Calls <see cref="Debugger.Break">Debugger.Break()</see>.
    /// </summary>
    public static void Break()
    {
#if MOD_DEBUGGER_ACTIVE
            Debugger.Break();
#endif
    }

    /// <summary>
    /// Calls <see cref="Debugger.IsLogging">Debugger.IsLogging()</see>.
    /// </summary>
    public static bool IsLogging()
    {
#if MOD_DEBUGGER_ACTIVE
            return Debugger.IsLogging();
#else
        return false;
#endif
    }

    /// <summary>
    /// Calls <see cref="Debugger.Launch">Debugger.Launch()</see>.
    /// </summary>
    public static bool Launch()
    {
#if MOD_DEBUGGER_ACTIVE
            return Debugger.Launch();
#else
        return false;
#endif
    }

    /// <summary>
    /// Calls <see cref="Debugger.Log(int, string, string)">Debugger.Log(int level, string category, string message)</see>.
    /// </summary>
    public static void Log(int level, string category, string message)
    {
#if MOD_DEBUGGER_ACTIVE
            Debugger.Log(level, category, message);
#endif
    }

    /// <summary>
    /// Calls <see cref="Debugger.NotifyOfCrossThreadDependency">Debugger.NotifyOfCrossThreadDependency()</see>.
    /// </summary>
    public static void NotifyOfCrossThreadDependency()
    {
#if MOD_DEBUGGER_ACTIVE
            Debugger.NotifyOfCrossThreadDependency();
#endif
    }

    #endregion
}