#region

using System;
using System.Collections.Generic;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Save
{
    public class SaveFile
    {
        private List<(string type, string json)> objects = new();
        private LinkedList<int> version = null;

        public SaveFile()
        {
        }

        public SaveFile(ISaveVersionParser parser)
        {
            version = parser.Parse(Application.version);
        }

        public SaveFile(LinkedList<int> version, List<(string type, string json)> objects)
        {
            this.version = version;
            this.objects = objects;
        }

        public void AddObject(Savable savable)
        {
            Type ty = savable.GetType();

            string assemblyName = ty.Assembly.GetName().Name;

            string qualifiedName = $"{ty.FullName}, {assemblyName}";

            objects.Add((qualifiedName, Json_Utilities.Serialize(savable)));
        }

        public SaveObject TryLoad()
        {
            Dictionary<Type, Savable> savables = new();

            Save_State state = Save_State.Valid;

            foreach (var valueTuple in objects)
            {
                (Savable s, Save_State st) =
                    Save_Utilities.DeserializeAndEvaluate(valueTuple.json, Type.GetType(valueTuple.type));

                state = (Save_State)Mathf.Max((int)state, (int)st);

                savables.Add(s.GetType(), s);
            }

            return new SaveObject(version, savables, state);
        }
    }
}