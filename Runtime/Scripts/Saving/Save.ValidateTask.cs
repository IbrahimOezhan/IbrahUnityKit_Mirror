using System;
using System.Text.Json;

namespace IbrahKit.Save
{
    internal partial class Save
    {
        private class ValidateTask
        {
            private SaveState fileState;

            private Savable result;

            public ValidateTask(string filePath, string fileContent, bool instantFail)
            {
                // File couldnt be parsed and therefor the type cannot be read and file is useless
                if (instantFail)
                {
                    fileState = SaveState.Corrupted;
                    result = null;
                    return;
                }

                Savable savable = Save_Utilities.GetSavable(fileContent);

                try
                {
                    result = Save_Utilities.GetDerivedSavable(fileContent, savable);

                    fileState = SaveState.Valid;
                }
                catch (JsonException)
                {
                    OnJsonException(filePath, savable);
                }
                catch
                {
                    fileState = SaveState.Corrupted;
                }
            }

            private void OnJsonException(string filePath, Savable savable)
            {
                Type t = Save_Utilities.GetSavableType(savable);

                if (t == null)
                {
                    // Type does not exist. File is useless
                    fileState = SaveState.Corrupted;

                    return;
                }

                try
                {
                    result = (Savable)Activator.CreateInstance(t);

                    fileState = SaveState.Outdated;
                }
                catch (Exception ex)
                {
                    IbrahDebug.LogWarning($"[{filePath}] {ex.Message}");

                    result = null;

                    fileState = SaveState.Corrupted;
                }
            }

            public SaveState GetFileState()
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