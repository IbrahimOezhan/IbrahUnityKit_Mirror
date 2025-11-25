using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IbrahKit.Debug
{
    /// <summary>
    /// A wrapper class for unitys standard debug class that adds additional functionality
    /// </summary>
    public static class IbrahDebug
    {
        public static bool DisableLogs { get; set; } = false;

        public static void Log(object message, Color c = default, Object context = null, [CallerMemberName] string caller = null)
        {
            if (c == default) c = Color.white;

            if (DisableLogs) return;

            string formattedMsg = c.UseOnString($"[Log] {message} (Caller: {caller})");

            if (context != null) UnityEngine.Debug.Log(formattedMsg, context);
            else UnityEngine.Debug.Log(formattedMsg);
        }

        public static void LogWarning(object message, Object context = null, [CallerMemberName] string caller = null)
        {
            if (DisableLogs) return;

            string formattedMsg = Color_Utilities.UseOnString(Color.yellow, $"[Warning] {message} (Caller: {caller})");

            if (context != null) UnityEngine.Debug.LogWarning(formattedMsg, context);
            else UnityEngine.Debug.LogWarning(formattedMsg);
        }

        public static void LogError(object message, Object context = null, [CallerMemberName] string caller = null)
        {
            if (DisableLogs) return;

            string formattedMsg = Color_Utilities.UseOnString(Color.red, $"[Error] {message} (Caller: {caller})");

            if (context != null) UnityEngine.Debug.LogError(formattedMsg, context);
            else UnityEngine.Debug.LogError(formattedMsg);
        }

        public static void LogException(Exception exception, Object context = null)
        {
            if (DisableLogs) return;

            if (context != null) UnityEngine.Debug.LogException(exception, context);
            else UnityEngine.Debug.LogException(exception);
        }
    }
}