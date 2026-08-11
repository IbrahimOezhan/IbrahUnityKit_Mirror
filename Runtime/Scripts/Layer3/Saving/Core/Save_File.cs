#region

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Save
{
    [Serializable]
    public class Save_File
    {
        [JsonInclude] private Dictionary<string, string> objects = new();

        [JsonInclude] private LinkedList<int> version = null;

        public Save_File()
        {
        }

        public Save_File(ISaveVersionParser parser)
        {
            version = parser.Parse(Application.version);
        }

        public Save_File(LinkedList<int> version, Dictionary<string, string> objects)
        {
            this.version = version;
            this.objects = objects;
        }

        public void AddObject(ISavable savable)
        {
            Type ty = savable.GetType();

            string assemblyName = ty.Assembly.GetName().Name;

            string qualifiedName = $"{ty.FullName}, {assemblyName}";

            objects.Add(qualifiedName, Json_Utilities.Serialize(savable));
        }

        public Save_Object TryLoad()
        {
            Dictionary<Type, ISavable> savables = new();

            Save_State state = Save_State.Valid;

            foreach (var valueTuple in objects)
            {
                Type t = Type.GetType(valueTuple.Key);

                if (t == null)
                {
                    throw new NullReferenceException("Type not found: " + valueTuple.Key);
                }

                (ISavable s, Save_State st) =
                    Save_Utilities.DeserializeAndEvaluate(valueTuple.Value, t);

                state = (Save_State)Mathf.Max((int)state, (int)st);

                savables.Add(s.GetType(), s);
            }

            return new Save_Object(version, savables, state);
        }
    }
}