using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace IbrahKit
{
    public static class Debug
    {
        public static bool DisableLogs { get; set; } = false;

        public static void Log(object message, Color c = default, UnityEngine.Object context = null, [CallerMemberName] string caller = null)
        {
            if (c == default) c = Color.white;

            if (DisableLogs) return;

            string formattedMsg = $"[Log] {message} (Caller: {caller})";

            formattedMsg = Color_Utilities.ColorString(formattedMsg, c);

            if (context != null) UnityEngine.Debug.Log(formattedMsg, context);
            else UnityEngine.Debug.Log(formattedMsg);
        }

        public static void LogWarning(object message, UnityEngine.Object context = null, [CallerMemberName] string caller = null)
        {
            if (DisableLogs) return;

            string formattedMsg = $"<color=yellow>[Warning] {message} (Caller: {caller})</color>";

            if (context != null) UnityEngine.Debug.LogWarning(formattedMsg, context);
            else UnityEngine.Debug.LogWarning(formattedMsg);
        }

        public static void LogError(object message, UnityEngine.Object context = null, [CallerMemberName] string caller = null)
        {
            if (DisableLogs) return;

            string formattedMsg = $"<color=red>[Error] {message} (Caller: {caller})</color>";

            if (context != null) UnityEngine.Debug.LogError(formattedMsg, context);
            else UnityEngine.Debug.LogError(formattedMsg);
        }

        public static void LogException(Exception exception, UnityEngine.Object context = null)
        {
            if (DisableLogs) return;

            if (context != null) UnityEngine.Debug.LogException(exception, context);
            else UnityEngine.Debug.LogException(exception);
        }
    }
}