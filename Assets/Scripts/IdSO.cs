using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ID")]
public class IdSO : ScriptableObject
{
    //Static Helper Functions
    public static T[] FindComponents<T>(GameObject source, IdSO tagToFind=null) where T : IHaveID
    {
        T[] components = source.GetComponentsInChildren<T>();
        if (components == null)
        {
            return null;
        }
        if(tagToFind == null)
        {
            return components;
        }
        else
        {
            return FindComponentsWithID<T>(source, tagToFind);
        }
    }
    public static T[] FindComponentsWithID<T>(GameObject source, IdSO tagToFind) where T : IHaveID
    {
        List<T> results = new List<T>();
        T[] components = source.GetComponentsInChildren<T>();
        if (components == null)
        {
            return null;
        }
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i].GetID() == tagToFind)
            {
                results.Add(components[i]);
                break;
            }
        }
        return results.ToArray();
    }
}

public interface IHaveID
{
    IdSO GetID();
}