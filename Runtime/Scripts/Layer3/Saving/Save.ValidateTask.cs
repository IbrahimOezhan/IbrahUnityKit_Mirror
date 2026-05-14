#region

using System;
using System.Text.Json;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.Save
{
    internal partial class Save
    {
        private class ValidateTask
        {
            private Save_State fileState;

            private Savable result;

            public ValidateTask(string filePath, string fileContent, bool instantFail)
            {
                // File couldn't be parsed and therefor the type cannot be read and file is useless
                if (instantFail)
                {
                    fileState = Save_State.Corrupted;
                    result = null;
                    return;
                }

                Savable savable = Save_Utilities.GetSavable(fileContent);

                try
                {
                    result = Save_Utilities.GetDerivedSavable(fileContent, savable);

                    fileState = Save_State.Valid;
                }
                catch (JsonException)
                {
                    OnJsonException(filePath, savable);
                }
                catch
                {
                    fileState = Save_State.Corrupted;
                }
            }

            private void OnJsonException(string filePath, Savable savable)
            {
                Type t = Save_Utilities.GetSavableType(savable);

                if (t == null)
                {
                    // Type does not exist. File is useless
                    fileState = Save_State.Corrupted;

                    return;
                }

                try
                {
                    result = (Savable)Activator.CreateInstance(t);

                    fileState = Save_State.Outdated;
                }
                catch (Exception ex)
                {
                    IbrahDebug.LogWarning($"[{filePath}] {ex.Message}");

                    result = null;

                    fileState = Save_State.Corrupted;
                }
            }

            public Save_State GetFileState()
            {
                return fileState;
            }

            public Savable GetSavable()
            {
                return result;
            }
        }
    }
}