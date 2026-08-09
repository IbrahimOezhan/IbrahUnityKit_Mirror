#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using IbrahKit.Save;
using IbrahKit.Utilities;
using Sirenix.Serialization;
using UnityEngine;

#endregion

[Serializable]
public class Save_Object
{
    [JsonInclude]
    [OdinSerialize] private Dictionary<Type, ISavable> objects;
    
    [JsonInclude]
    [SerializeField] private Save_State state;
    
    [JsonInclude]
    [OdinSerialize] private LinkedList<int> version;

    public Save_Object(LinkedList<int> version, Dictionary<Type, ISavable> objects, Save_State state)
    {
        this.state = state;
        this.version = version;
        this.objects = objects;
    }

    public T Get<T>() where T : ISavable, new()
    {
        if (objects.TryGetValue(typeof(T), out ISavable savable))
        {
            return (T)savable;
        }
        else
        {
            T newObject = new T();
            
            objects.Add(typeof(T), newObject);
            return newObject;
        }
    }

    public void Put<T>(T savable) where T : ISavable
    {
        objects[savable.GetType()] = savable;
    }

    public Save_File ToSaveFile()
    {
        return new Save_File(version, objects.ToDictionary(x =>  Save_Utilities.GetQualifiedName(x.Key), y => Json_Utilities.Serialize(y.Value,y.Key)));
    }

    public Save_State GetSaveState()
    {
        return state;
    }

    public LinkedList<int> GetVersion()
    {
        return version;
    }

    public int CompareTo(Save_Object other)
    {
        LinkedList<int> otherVersion = other.GetVersion();

        LinkedListNode<int> node = version.First;
        LinkedListNode<int> node2 = otherVersion.First;

        if (node == null && node2 == null) throw new NullReferenceException("Both versions have no elements");

        while (node != null && node2 != null)
        {
            if (node.Value < node2.Value)
            {
                return -1;
            }
            else if (node2.Value < node.Value)
            {
                return 1;
            }

            node = node.Next;
            node2 = node2.Next;
        }

        return node != null ? 1 : node2 != null ? 1 : 0;
    }
}