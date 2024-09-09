using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CharactersMapper
{
    private static readonly Dictionary<string, HashSet<GameObject>> _charactersMapping = new();

    public static void SetCharacter(string id, GameObject gameObject)
    {
        if (_charactersMapping.ContainsKey(id))
        {
            _charactersMapping[id].Add(gameObject);
        }
        else
        {
            _charactersMapping[id] = new HashSet<GameObject> { gameObject };
        }
    }

    public static void RemoveCharacter(string id, GameObject gameObject)
    {
        if (_charactersMapping.TryGetValue(id, out var gameObjects))
        {
            gameObjects.Remove(gameObject);
        }
    }

    public static IEnumerable<GameObject> GetMappedObjects(string id)
    {
        if (_charactersMapping.TryGetValue(id, out var objects))
        {
            return objects;
        }
        return Enumerable.Empty<GameObject>();
    }
}
