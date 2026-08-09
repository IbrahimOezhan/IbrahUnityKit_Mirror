#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Save;
using IbrahKit.Utilities;

#endregion

public class Save_Object
{
    private Dictionary<Type, Savable> objects;
    private Save_State state;
    private LinkedList<int> version;

    public Save_Object(LinkedList<int> version, Dictionary<Type, Savable> objects, Save_State state)
    {
        this.state = state;
        this.version = version;
        this.objects = objects;
    }

    public T Get<T>(T @default) where T : Savable
    {
        if (objects.TryGetValue(typeof(T), out Savable savable))
        {
            return (T)savable;
        }
        else
        {
            objects.Add(typeof(T), @default);
            return @default;
        }
    }

    public void Put<T>(T savable) where T : Savable
    {
        objects[savable.GetType()] = savable;
    }

    public Save_File ToSaveFile()
    {
        return new Save_File(version, objects.Select(x =>
            (Save_Utilities.GetQualifiedName(x.Key), Json_Utilities.Serialize(x.Value))).ToList());
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