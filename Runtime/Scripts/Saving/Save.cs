using IbrahKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Save
{
    private string key;

    private string folderPath;

    private string version;

    private State state = State.Valid;

    private string[] filePaths = new string[0];

    private bool[] encrypted;

    private State[] fileState;

    private HashSet<Savable> inUse = new();

    private Dictionary<string, Savable> loadable = new();

    public void Init(string folderPath, string key)
    {
        this.folderPath = folderPath;

        this.key = key;

        version = Path.GetFileName(folderPath);
    }

    public Save(List<string> names, List<Savable> savables, string folderPath, string key, bool encrypt)
    {
        Init(folderPath, key);

        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }

        Directory.CreateDirectory(folderPath);

        for (int i = 0; i < savables.Count; i++)
        {
            Return(names[i], savables[i], encrypt);
            loadable.Add(names[i], savables[i]);
        }
    }

    public Save(string folderPath, string key)
    {
        Init(folderPath, key);

        filePaths = Directory.GetFiles(folderPath);

        string[] fileContents = new string[filePaths.Length];

        fileState = new State[filePaths.Length];

        bool[] validDecrypt = new bool[filePaths.Length];

        encrypted = new bool[filePaths.Length];

        ValidateTask[] outdatedValidation = new ValidateTask[filePaths.Length];

        for (int i = 0; i < fileContents.Length; i++)
        {
            fileContents[i] = File.ReadAllText(filePaths[i]);
        }

        for (int i = 0; i < validDecrypt.Length; i++)
        {
            (validDecrypt[i], encrypted[i]) = Save_Utilities.Decrypt(fileContents[i], key, out fileContents[i]);

            if (!validDecrypt[i])
            {
                fileState[i] = State.Corrupted;
            }
        }

        for (int i = 0; i < outdatedValidation.Length; i++)
        {
            outdatedValidation[i] = new(fileContents[i], fileState[i] == State.Corrupted);

            fileState[i] = outdatedValidation[i].GetFileState();

            state = (State)MathF.Max((int)state, (int)fileState[i]);
        }

        for (int i = 0; i < outdatedValidation.Length; i++)
        {
            if (fileState[i] != State.Corrupted) loadable.Add(Path.GetFileName(filePaths[i]), outdatedValidation[i].GetSavable());
        }
    }

    public int GetValidFileCount()
    {
        int amount = 0;

        for (int i = 0; i < fileState.Length; i++)
        {
            if (fileState[i] == State.Valid) amount++;
        }

        return amount;
    }

    public State GetState()
    {
        return state;
    }

    public List<string> GetKeys()
    {
        return loadable.Keys.ToList();
    }

    public List<Savable> GetSavables()
    {
        return loadable.Values.ToList();
    }

    public void Delete()
    {
        Directory.Delete(folderPath, true);
    }

    public Savable Load(string name, Savable defaultValue)
    {
        if (loadable.TryGetValue(name, out Savable savable))
        {
            if (inUse.Contains(savable))
            {
                //InUse error
                throw new SaveInUseException();
            }

            inUse.Add(savable);

            return savable;
        }
        else
        {
            loadable.Add(name, defaultValue);
            inUse.Add(defaultValue);

            //Does not exist . New save json
            return defaultValue;
        }
    }

    public void Return(string name, Savable value, bool encrypt)
    {
        string json = JsonSerializer.Serialize(value, Options);

        string fileContent = encrypt ? String_Utilities.DecryptEncrypt(json, key) : json;

        using StreamWriter streamWriter = new(Path.Combine(folderPath, name));

        streamWriter.Write(fileContent);

        inUse.Remove(value);
    }

    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true,
        WriteIndented = true,
    };

    public int CompareTo(Save otherSave)
    {
        return String_Utilities.CompareVersions(version, otherSave.version);
    }

    private class ValidateTask
    {
        private State fileState;

        private Savable result;

        public ValidateTask(string fileContent, bool instantFail)
        {
            // File couldnt be parsed and therefor the type cannot be read and file is useless
            if (instantFail)
            {
                fileState = State.Corrupted;
                result = null;
                return;
            }

            Savable savable = Save_Utilities.GetSavable(fileContent);

            try
            {
                result = Save_Utilities.GetDerivedSavable(fileContent, savable);

                fileState = State.Valid;
            }
            catch (JsonException e)
            {
                Type t = Save_Utilities.GetSavableType(savable);

                if (t == null)
                {
                    // Type does not exist. File is useless
                    fileState = State.Corrupted;
                    return;
                }

                result = (Savable)Activator.CreateInstance(t);

                fileState = State.Outdated;
            }
            catch
            {
                fileState = State.Corrupted;
            }
        }

        public State GetFileState()
        {
            return (fileState);
        }

        public Savable GetSavable()
        {
            return result;
        }
    }

    public enum State
    {
        Valid = 0,
        Outdated = 1,
        Corrupted = 2,
    }
}
