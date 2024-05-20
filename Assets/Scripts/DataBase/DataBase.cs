using System.Collections.Generic;
using UnityEngine;

public abstract class DataBase<T> : ScriptableObjectSingleton<DataBase<T>> where T : IDataBaseEntry
{
    [SerializeField]
    private List<T> _items;
    public IEnumerable<T> Items => _items;

    public static T FindById(string id)
    {
        return Instance.FindByIdInternal(id);
    }

    private T FindByIdInternal(string id)
    {
        return _items.Find(x => x.Id == id);
    }

    private void OnValidate()
    {
        var itemsHashSet = new HashSet<string>();
        foreach (var item in _items)
        {
            if (string.IsNullOrWhiteSpace(item.Id) || !itemsHashSet.Add(item.Id))
            {
                item.GenerateId();
            }
        }
    }
}
