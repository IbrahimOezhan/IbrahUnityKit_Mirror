#region

using System;
using System.Runtime.CompilerServices;
using IbrahKit.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace IbrahKit.Debugging
{
    /// <summary>
    /// A wrapper class for unitys standard debug class that adds additional functionality
    /// </summary>
    public static class IbrahDebug
    {
        public static void Log(object message, Color c = default, Object context = null,
            [CallerMemberName] string caller = null)
        {
            if (c == default) c = Color.white;
            string formattedMsg = c.UseOnString($"[Log] {message} (Caller: {caller})");

            if (context != null) Debug.Log(formattedMsg, context);
            else Debug.Log(formattedMsg);
        }

        public static void LogWarning(object message, Object context = null, [CallerMemberName] string caller = null)
        {
            string formattedMsg = Color_Utilities.UseOnString(Color.yellow, $"[Warning] {message} (Caller: {caller})");

            if (context != null) Debug.LogWarning(formattedMsg, context);
            else Debug.LogWarning(formattedMsg);
        }

        public static void LogError(object message, Object context = null, [CallerMemberName] string caller = null)
        {
            string formattedMsg = Color_Utilities.UseOnString(Color.red, $"[Error] {message} (Caller: {caller})");

            if (context != null) Debug.LogError(formattedMsg, context);
            else Debug.LogError(formattedMsg);
        }

        public static void LogException(Exception exception, Object context = null)
        {
            if (context != null) Debug.LogException(exception, context);
            else Debug.LogException(exception);
        }
    }
}